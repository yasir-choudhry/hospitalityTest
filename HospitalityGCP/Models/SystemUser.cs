using Microsoft.AspNetCore.Http;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace HospitalityGCP.Models
{
    public class SystemUser
    {
        [Key]
        public int UserID { get; set; }

        [DisplayName("First Name")]
        [Required]
        public string UserFirstName { get; set; }

        [DisplayName("Last Name")]
        [Required]
        public string UserLastName { get; set; }

        [DisplayName("Email")]
        [Required]
        [EmailAddress]
        public string UserEmail { get; set; }

        [DisplayName("Admin User")]
        [UserSelect]
        public bool IsAdmin { get; set; }

        [DisplayName("Caterer")]
        public bool IsCaterer { get; set; }

        [DisplayName("Ethos Name")]
        [Required]
        public string EthosName { get; set; }

        //Blank constructor used for impersonate function. Could do with a smarter impersonate function at some point.
        public SystemUser()
        {
            IsAdmin = false;
            IsCaterer = false;
            UserEmail = "someone@dft.gov.uk";
        }

        //Method receives a dictionary of the HTTP Headers
        public static string GetCurrentUserEmail()
        {
            IHeaderDictionary aDict = WebHelpers.HttpContext.Request.Headers;
            string Result = "someone@dft.gov.uk";

            //Check whether the header that we need is included in the dictionary
            if (aDict.Where(x => x.Key == "X-Goog-Authenticated-User-Email").Count() > 0)
            {
                //if it is then take the value, tostring it and assign to the result
                Result = aDict.Where(x => x.Key == "X-Goog-Authenticated-User-Email").First().Value.ToString();

                //tidy up the string to remove the prefix and just leave the email address
                Result = Result.Replace("accounts.google.com:", string.Empty);
            }

            return Result;
        }

    }
}