using HospitalityGCP.Data;
using HospitalityGCP.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Notify.Client;
using Notify.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HospitalityGCP.Controllers
{
    public class BasketController : Controller
    {
        private readonly HospitalityGCPContext _context;
        private readonly ConfigSettings _myConfig;

        public BasketController(HospitalityGCPContext context, IOptions<ConfigSettings> myConfig)
        {
            _context = context;
            _myConfig = myConfig.Value;
        }

        public async Task<IActionResult> ShowBasket()
        {
            ViewModelShowBasket theModel = new ViewModelShowBasket();

            theModel.MenuItems = await _context.MenuItems.OrderBy(m => m.Position).Where(m => !m.RFD).ToListAsync();
            theModel.ShowItems = await _context.Basket.Where(m => m.Email == SystemUser.GetCurrentUserEmail()).ToListAsync();

            if (theModel == null)
            {
                return NotFound();
            }
            return View(theModel);
        }

        //ADD item to basket PLUS if an item is already in basket and added again only qty updated - remove texttttt
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<RedirectToActionResult> ShowBasket(int BasketID, int MenuItemID, string MenuItemName, string MenuItemDesc, decimal MenuItemPrice, int Quantity, TimeSpan ItemTime, string NoticeReq)
        {
            var iteminbasket = await _context.Basket.SingleOrDefaultAsync(x => x.MenuItemID == MenuItemID && x.ItemTime == ItemTime && x.Email == SystemUser.GetCurrentUserEmail());

            if (iteminbasket == null || iteminbasket != null && iteminbasket.ItemTime != ItemTime)
            {
                Basket aBasket = new Basket();
                aBasket.Email = SystemUser.GetCurrentUserEmail();
                aBasket.MenuItemID = MenuItemID;
                aBasket.MenuItemName = MenuItemName;
                aBasket.MenuItemDesc = MenuItemDesc;
                aBasket.MenuItemPrice = MenuItemPrice;
                aBasket.Quantity = Quantity;
                aBasket.ItemTime = ItemTime;
                aBasket.NoticeReq = NoticeReq;

                _context.Add(aBasket);
                await _context.SaveChangesAsync();
            }
            else
            {
                iteminbasket.Quantity = iteminbasket.Quantity += Quantity;
                iteminbasket.ItemTime = ItemTime;
                _context.Update(iteminbasket);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("ShowBasket", "Basket");
        }

        //UPDATE Item or time in basket
        [HttpPost]
        public async Task<IActionResult> UpdateItem(int BasketID, int Quantity, TimeSpan ItemTime)
        {
            var updateitem = await _context.Basket.SingleOrDefaultAsync(x => x.BasketID == BasketID);
            updateitem.Quantity = Quantity;
            updateitem.ItemTime = ItemTime;
            _context.Update(updateitem);
            await _context.SaveChangesAsync();
            return RedirectToAction("ShowBasket", "Basket");
        }

        //REMOVE item from users basket
        [HttpPost]
        public async Task<IActionResult> RemoveItem(int BasketID)
        {
            var removeitem = await _context.Basket.SingleOrDefaultAsync(x => x.BasketID == BasketID);
            _context.Remove(removeitem);
            await _context.SaveChangesAsync();
            return RedirectToAction("ShowBasket", "Basket");
        }

        //EMPTY users basket
        public IActionResult EmptyBasket()
        {
            var row = (from x in _context.Basket.Where(x => x.Email == SystemUser.GetCurrentUserEmail()) select x);
            _context.Basket.RemoveRange(row);
            _context.SaveChanges();
            return RedirectToAction("ShowBasket", "Basket");
        }

        //Allows a User to view their current and closed orders
        public async Task<IActionResult> ViewOrder()
        {
            List<ViewModelOrdersList> vieworders = await _context.ViewModelOrdersList
                .OrderBy(m => m.OrderDate)
                .Where(m => m.UserEmail == SystemUser.GetCurrentUserEmail()).ToListAsync();

            if (vieworders == null)
            {
                return NotFound();
            }

            return View(vieworders);
        }

        public IActionResult OrderConfirmed(int OrderID)
        {
            ViewBag.OrderRef = OrderID.ToString();
            return View();
        }

        //DDL for rooms
        public IActionResult PlaceOrder()
        {
            List<Rooms> roomlist = new List<Rooms>();
            roomlist = (from row in _context.Rooms select row).Where(x => !x.RFD).OrderBy(x => x.RoomDesc).ToList();
            ViewBag.ListOfRooms = roomlist;
            return View();
        }

        //ADD PlaceOrder Details to OrdersTable in DATABASE add Basket items to OrderedItems then clear Basket and send email
        [HttpPost]
        public async Task<IActionResult> PlaceOrder([Bind("UserName,UserEmail,UserPhone,HostName,HostEmail,HostPhone,CostCentre,OrderDate," +
            "MeetingStartTime,MeetingEndTime,NumAttendees,RoomID,DietaryReq,Notes")] Order newcheckout)
        {
            if (ModelState.IsValid)
            {
                List<Basket> userorder = await _context.Basket.Where(m => m.Email == SystemUser.GetCurrentUserEmail()).ToListAsync();

                //create variable for total cost
                decimal? baskettotal = 0;

                //loop through userorder with foreach to calculate the total for each basket item and add to total cost as you go.
                foreach (Basket total in userorder)
                {
                    decimal itemTotal = total.MenuItemPrice * total.Quantity;
                    baskettotal = baskettotal + itemTotal;
                }

                // assign total cost to newcheckout.BasketTotal
                newcheckout.RequestedDateTime = DateTime.Now;
                newcheckout.BasketTotal = baskettotal;
                newcheckout.OrderStatusID = 1;
                _context.Add(newcheckout);
                await _context.SaveChangesAsync();

                var roomdesc = (from x in _context.Rooms.Where(x => x.RoomID == newcheckout.RoomID) select x.RoomDesc).FirstOrDefault();
                               
                OrderedItems finalorder = new OrderedItems();
                foreach (var item in userorder)
                {
                    finalorder.OrderID = newcheckout.OrderID;
                    finalorder.MenuItemID = item.MenuItemID;
                    finalorder.Qty = item.Quantity;
                    finalorder.MenuItemPrice = item.MenuItemPrice;
                    finalorder.ItemTime = item.ItemTime;

                    _context.OrderedItems.Add(finalorder);
                    await _context.SaveChangesAsync();
                }

                //EMAIL - Create the client that will be used to send the email. pass it the api key which is held in appsetting.json
                NotificationClient client = new NotificationClient(_myConfig.MailAPIKey);

                Dictionary<string, dynamic> emailData = new Dictionary<string, dynamic>();
                emailData.Add("SubjectText", "Confirmation of Hospitality order Ref: " + newcheckout.OrderID);
                emailData.Add("BodyText", "Thank you for your order. Please find summary below.\r\n\r\nYour order ref:  " + newcheckout.OrderID);
                emailData.Add("Total", "Total price of order = £" + userorder.Sum(x => x.MenuItemPrice * x.Quantity));
                emailData.Add("Contact", "Caterer contact info: \r\n Email: hospitality_dft@dft.gov.uk \r\n Phone: 020 7944 2864");

                List<string> details = new List<string>();
                details.Add("Name - " + newcheckout.UserName);
                details.Add("Email - " + newcheckout.UserEmail);
                details.Add("Phone - " + newcheckout.UserPhone);
                details.Add("Costcentre - " + newcheckout.CostCentre);
                details.Add("Number of attendees - " + newcheckout.NumAttendees);
                details.Add("Host name - " + newcheckout.HostName);
                details.Add("Host email - " + newcheckout.HostEmail);
                details.Add("Host phone - " + newcheckout.HostPhone);
                details.Add("Meeting date - " + newcheckout.OrderDate.ToString("dd'/'MM'/'yyyy"));
                details.Add("Meeting start time - " + newcheckout.MeetingStartTime.ToString("hh\\:mm"));
                details.Add("Meeting end time - " + newcheckout.MeetingEndTime.ToString("hh\\:mm"));
                details.Add("Room - " + roomdesc);
                details.Add("Dietary requirements - " + newcheckout.DietaryReq);
                details.Add("Notes - " + newcheckout.Notes);

                emailData.Add("Details", details);

                List<string> order = new List<string>();
                foreach (var item in userorder)
                {
                    order.Add("Item - " + item.MenuItemName);
                    order.Add("Description - " + item.MenuItemDesc);
                    order.Add("Delivery time - " + item.ItemTime.ToString("hh\\:mm"));
                    order.Add("Quantity - " + item.Quantity);
                    order.Add("Price - £" + item.MenuItemPrice);
                    order.Add("Total cost of items - £" + (item.Quantity * item.MenuItemPrice));
                    order.Add(" ");
                }

                emailData.Add("Order", order);

                List<string> emails = new List<string>();
                emails.Add(newcheckout.UserEmail);
                emails.Add(_myConfig.HospitalityTeamEmail);
                if (newcheckout.HostEmail != null)
                {
                    emails.Add(newcheckout.HostEmail);
                }

                for (int i = 0; i < emails.Count; i++)
                {
                    EmailNotificationResponse response = client.SendEmail(emails[i], _myConfig.EmailTemplates.ConfirmTemplate, emailData);
                }

                //EMPTY basket table of users items
                var row = (from x in _context.Basket.Where(x => x.Email == SystemUser.GetCurrentUserEmail()) select x);
                _context.Basket.RemoveRange(row);
                _context.SaveChanges();

                return RedirectToAction("OrderConfirmed", "Basket", new { OrderID = newcheckout.OrderID });
            }

            List<Rooms> roomlist = new List<Rooms>();
            roomlist = (from row in _context.Rooms select row).Where(x => !x.RFD).ToList();
            ViewBag.ListOfRooms = roomlist;
            return View(newcheckout);
        }

        //CANCEL order
        public async Task<IActionResult> CancelOrder(int id)
        {
            var cancelorder = await _context.Order.SingleOrDefaultAsync(m => m.OrderID == id);
            cancelorder.OrderStatusID = 3;
            _context.Update(cancelorder);
            await _context.SaveChangesAsync();

            NotificationClient client = new NotificationClient(_myConfig.MailAPIKey);
            Dictionary<string, dynamic> emailData = new Dictionary<string, dynamic>();
            emailData.Add("SubjectText", "CANCELLATION of Hospitality order Ref: " + id);
            emailData.Add("BodyText", "Your order, Ref: " + id + " has been CANCELLED.\r\n");
            emailData.Add("Contact", "Caterer contact info: \r\n Email: hospitality_dft@dft.gov.uk \r\n Phone: 020 7944 2864");

            List<string> emails = new List<string>();
            emails.Add(cancelorder.UserEmail);
            emails.Add(_myConfig.HospitalityTeamEmail); 
            if (cancelorder.HostEmail != null)
            {
                emails.Add(cancelorder.HostEmail);
            }

            for (int i = 0; i < emails.Count; i++)
            {
                EmailNotificationResponse response = client.SendEmail(emails[i], _myConfig.EmailTemplates.CancelTemplate, emailData);
            }
            return RedirectToAction("ViewOrder", "Basket");
        }
    }
}
