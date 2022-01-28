using HospitalityGCP.Data;
using HospitalityGCP.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HospitalityGCP.Controllers
{
    public class CatererController : Controller
    {
        private readonly HospitalityGCPContext _context;
        private SystemUser _user;

        public CatererController(HospitalityGCPContext context)
        {
            _context = context;

            //Call 'GetCurrentUserEmail' method of AmendUser class to get the users email address from the HTTP headers if available
            string userEmail = SystemUser.GetCurrentUserEmail();

            //and then load the AmendUser using the result
            _user = _context.SystemUser.Where(x => x.UserEmail == userEmail).FirstOrDefault();

            //If users email not available in HTTP headers (because you're playing solution locally rather than published on app engine)
            //then theUser will be null. In that case, just load the default AmendUser which we're using for basic impersonation.    
            if (_user == null)
            {
                _user = new SystemUser();
                _user.UserEmail = userEmail;
            }
        }

        public IActionResult TodaysOrders()
        {
            if (_user.IsCaterer || _user.IsAdmin)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Disallowed", "Home");
            }            
        }

        public IActionResult OpenOrders()
        {
            if (_user.IsCaterer || _user.IsAdmin)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Disallowed", "Home");
            }
        }

        public IActionResult ClosedOrders()
        {
            if (_user.IsCaterer || _user.IsAdmin)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Disallowed", "Home");
            }
        }

        public async Task<IActionResult> OrderDetails(int? id)
        {
            if (_user.IsCaterer || _user.IsAdmin)
            {
                if (id == null)
                {
                    return NotFound();
                }

                ViewModelOrderDetails theOrder = new ViewModelOrderDetails();

                theOrder.OrdersDetails = _context.ViewModelOrdersList.FirstOrDefault(m => m.OrderID == id);
                theOrder.OrderedItems = await _context.ViewModelOrderedItems.OrderBy(m => m.ItemTime).Where(m => m.OrderID == id).ToListAsync();

                if (theOrder == null)
                {
                    return NotFound();
                }

                return View(theOrder);
            }
            else
            {
                return RedirectToAction("Disallowed", "Home");
            }
        }

        public async Task<IActionResult> CloseOrder(int? id)
        {
            if (_user.IsCaterer || _user.IsAdmin)
            {
                var record = await _context.Order.SingleOrDefaultAsync(m => m.OrderID == id);
                record.OrderStatusID = 2;
                await _context.SaveChangesAsync();
                return RedirectToAction("OpenOrders", "Caterer");
            }
            else
            {
                return RedirectToAction("Disallowed", "Home");
            }
        }

        public IActionResult Reports()
        {
            if (_user.IsCaterer || _user.IsAdmin)
            {
                ViewModelCatererReport a = new ViewModelCatererReport();
                return View(a);
            }
            else
            {
                return RedirectToAction("Disallowed", "Home");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Reports(DateTime startdate, DateTime enddate, string submit)
        {
            if (_user.IsCaterer || _user.IsAdmin)
            {
                DateTime defaultDate = DateTime.MinValue;

                if (startdate == defaultDate || enddate == defaultDate)
                {
                    ViewModelCatererReport a = new ViewModelCatererReport();
                    return View(a);
                }

                //Get the data you want to export to excel

                if (submit == "Export to Excel")
                {
                    var aReport = _context.ViewModelOrdersList.OrderBy(x => x.OrderDate)
                    .Where(x => x.OrderDate >= startdate && x.OrderDate <= enddate && x.OrderStatus == "Closed")
                    .Select(x => new { OrderDate = x.OrderDate.ToString("dd-MM-yyyy"), x.UserName, x.RoomDesc, x.CostCentre, OrderID = x.OrderID.ToString(), x.BasketTotal }).ToList();
                    
                    //call method in excelexport class to build the byte array
                    if (aReport.Count > 0)
                    {
                        //Add a row to the end just to show the total of the basket totals.
                        aReport.Insert(aReport.Count, 
                            new { OrderDate = "", UserName = "", RoomDesc = "", CostCentre = "", OrderID = "Total:", BasketTotal = aReport.Sum(x => x.BasketTotal) });

                        byte[] result = ExcelExport.BuildExportPackage(aReport);
                        return File(result, "application/ms-excel", $"Hospitality.xlsx");
                    }

                    ViewModelCatererReport a = new ViewModelCatererReport();
                    a.startDate = startdate;
                    a.endDate = enddate;
                    return View(a);
                }
                else
                {
                    List<ViewModelOrdersList> aReport = await _context.ViewModelOrdersList.OrderBy(x => x.OrderDate)
                    .Where(x => x.OrderDate >= startdate && x.OrderDate <= enddate && x.OrderStatus == "Closed").ToListAsync();
                    ViewModelCatererReport a = new ViewModelCatererReport();
                    a.OrdersList = aReport;
                    a.startDate = startdate;
                    a.endDate = enddate;
                    return View(a);
                }
            }
            else
            {
                return RedirectToAction("Disallowed", "Home");
            }
        }

    }
}