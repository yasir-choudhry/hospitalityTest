using System.ComponentModel.DataAnnotations;

namespace HospitalityGCP.Models
{
    public class UserSelectAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            SystemUser aUser = (SystemUser)validationContext.ObjectInstance;

            if (aUser.IsAdmin == false && aUser.IsCaterer == false)
            {
                return new ValidationResult("Please select either an Admin or Caterer user");
            }

            return ValidationResult.Success;
        }
    }
}
