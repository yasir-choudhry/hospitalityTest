using HospitalityGCP.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace HospitalityGCP.ViewComponents
{
    public class OrdersListViewComponent : ViewComponent
    {
        private readonly HospitalityGCPContext _context;

        public OrdersListViewComponent(HospitalityGCPContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync(string Thetype)
        {
            switch (Thetype)
            {
                case "Today":
                    return View(await _context.ViewModelOrdersList.OrderBy(x => x.MeetingStartTime).Where(x => x.OrderDate == DateTime.Today && x.OrderStatus == "Open").ToListAsync());
                case "Open":
                    return View(await _context.ViewModelOrdersList.Where(x => x.OrderStatus == "Open").ToListAsync());
                case "Closed":
                    return View(await _context.ViewModelOrdersList.Where(x => x.OrderStatus == "Closed" || x.OrderStatus == "Cancelled").ToListAsync());
                default:
                    return View(await _context.ViewModelOrdersList.ToListAsync());
            }
        }
    }
}
