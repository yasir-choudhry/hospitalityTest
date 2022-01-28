using HospitalityGCP.Data;
using HospitalityGCP.Models;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Linq;

namespace HospitalityGCP.Controllers
{
    public class HomeController : Controller
    {
        private readonly HospitalityGCPContext _context;

        public HomeController(HospitalityGCPContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            //Call 'GetCurrentUserEmail' method of AmendUser class to get the users email address from the HTTP headers if available
            //and then load the AmendUser using the result
            SystemUser theUser = _context.SystemUser.Where(x => x.UserEmail == SystemUser.GetCurrentUserEmail()).FirstOrDefault();

            //If users email not available in HTTP headers (because you're playing solution locally rather than published on app engine)
            //then theUser will be null. In that case, just load the default AmendUser which we're using for basic impersonation.            
            if (theUser == null)
            {
                theUser = new SystemUser();
            }

            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Disallowed()
        {
            return View();
        }

        public IActionResult IndexUser()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            var ex = HttpContext.Features.Get<IExceptionHandlerFeature>();
            ErrorViewModel vModel = new ErrorViewModel();
            vModel.RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
            vModel.Message = ex.Error.Message;
            vModel.StackTrace = ex.Error.StackTrace.Replace("\r\n", "<br /><br />");

            return View(vModel);
        }
    }
}
