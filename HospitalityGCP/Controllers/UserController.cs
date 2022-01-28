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
    public class UserController : Controller
    {
        private readonly HospitalityGCPContext _context;

        public UserController(HospitalityGCPContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> UserOrderDetails(int? id)
        {
           
            if (id == null)
            {
                return NotFound();
            }

            ViewModelOrderDetails theOrder = new ViewModelOrderDetails();

            theOrder.OrdersDetails = _context.ViewModelOrdersList.FirstOrDefault(m => m.OrderID == id);
            theOrder.OrderedItems = await _context.ViewModelOrderedItems.Where(m => m.OrderID == id).ToListAsync();

            if (theOrder.OrdersDetails == null)
            {
                return RedirectToAction("Disallowed", "Home");
            }

            if (theOrder.OrdersDetails.UserEmail == SystemUser.GetCurrentUserEmail())
            { 
            return View(theOrder);
            }

            return RedirectToAction("Disallowed", "Home");
        }

        public IActionResult MonthlySpend()
        {
            ViewModelUserReport a = new ViewModelUserReport();
            return View(a);
        }

        [HttpPost]
        public async Task<IActionResult> MonthlySpend(DateTime reportdate)
        {
            List<ViewModelOrdersList> monthlyspend = await _context.ViewModelOrdersList
                .OrderBy(m => m.OrderDate)
                .Where(m => m.UserEmail == SystemUser.GetCurrentUserEmail() 
                && m.OrderStatus == "Closed" && reportdate.Year == m.OrderDate.Year 
                && reportdate.Month == m.OrderDate.Month).ToListAsync();

            ViewModelUserReport a = new ViewModelUserReport();
            a.OrdersList = monthlyspend;
            a.reportDate = reportdate;
            
            if (monthlyspend == null)
            {
                return NotFound();
            }
            return View(a);
        }

        public IActionResult Impersonate()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ChangeImpersonation(string UserTypeID)
        {
            var theUser = _context.SystemUser.Where(x => x.UserEmail == SystemUser.GetCurrentUserEmail()).FirstOrDefault();

            if (theUser != null)
            {
                switch (UserTypeID)
                {
                    case "1"://Standard user
                        theUser.IsAdmin = false;
                        theUser.IsCaterer = false;
                        break;
                    case "2"://Admin
                        theUser.IsAdmin = true;
                        theUser.IsCaterer = false;
                        break;
                    case "3"://Caterer
                        theUser.IsAdmin = false;
                        theUser.IsCaterer = true;
                        break;
                    default:
                        break;
                }
            }

            _context.Update(theUser);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index", "Home");
        }
    }
}