using System.ComponentModel.DataAnnotations;

namespace HospitalityGCP.Models
{
    public class HostNameAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            Order aHost = (Order)validationContext.ObjectInstance;

            if (aHost.HostName == null && aHost.HostEmail != null || aHost.HostName == null && aHost.HostPhone != null)
            {
                return new ValidationResult("Please enter a host name");
            }

            return ValidationResult.Success;
        }
    }
}
