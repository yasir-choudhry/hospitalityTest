using System.ComponentModel.DataAnnotations;

namespace HospitalityGCP.Models
{
    public class HostPhoneAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            Order aHost = (Order)validationContext.ObjectInstance;

            if (aHost.HostPhone == null && aHost.HostEmail != null || aHost.HostPhone == null && aHost.HostName != null)
            {
                return new ValidationResult("Please enter a host phone number");
            }

            return ValidationResult.Success;
        }
    }
}
