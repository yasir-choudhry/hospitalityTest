using HospitalityGCP.Data;
using HospitalityGCP.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Linq;

namespace HospitalityGCP.ViewComponents
{
    public class MenuViewComponent : ViewComponent
    {
        private readonly HospitalityGCPContext _context;
        private readonly ConfigSettings _myConfig;

        public MenuViewComponent(HospitalityGCPContext context, IOptions<ConfigSettings> myConfig)
        {
            _context = context;
            _myConfig = myConfig.Value;
        }

        public IViewComponentResult Invoke()
        {
            ViewBag.ImpersonateOn = _myConfig.ImpersonateOn;
            
            //Call 'GetCurrentUserEmail' method of AmendUser class to get the users email address from the HTTP headers if available
            string userEmail = SystemUser.GetCurrentUserEmail();
            
            //and then load the AmendUser using the result
            SystemUser theUser = _context.SystemUser.Where(x => x.UserEmail == userEmail).FirstOrDefault();

            //If users email not available in HTTP headers (because you're playing solution locally rather than published on app engine)
            //then theUser will be null. In that case, just load the default AmendUser which we're using for basic impersonation.    
            if (theUser == null)
            {
                theUser = new SystemUser();
                theUser.UserEmail = userEmail;
            }

            return View(theUser);
        }
    }
}

