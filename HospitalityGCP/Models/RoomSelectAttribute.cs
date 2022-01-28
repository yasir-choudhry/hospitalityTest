using System.ComponentModel.DataAnnotations;

namespace HospitalityGCP.Models
{
    public class RoomSelectAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            Order aRoom = (Order)validationContext.ObjectInstance;
            
            if (aRoom.RoomID == 0)
            {
                return new ValidationResult("Please select a valid location for delivery of order");
            }

            return ValidationResult.Success;
        }
    }
}
