using System.ComponentModel.DataAnnotations;

namespace HospitalityGCP.Models
{
    public class HostEmailAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            Order aHost = (Order)validationContext.ObjectInstance;

            if (aHost.HostEmail == null && aHost.HostName != null || aHost.HostEmail == null && aHost.HostPhone != null)
            {
                return new ValidationResult("Please enter a host email");
            }

            return ValidationResult.Success;
        }
    }
}
