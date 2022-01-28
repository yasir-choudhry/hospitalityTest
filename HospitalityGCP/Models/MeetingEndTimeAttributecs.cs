using System.ComponentModel.DataAnnotations;

namespace HospitalityGCP.Models
{
    public class MeetingEndTimeAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            Order endTime = (Order)validationContext.ObjectInstance;

            if (endTime.MeetingEndTime <= endTime.MeetingStartTime)
            {
                return new ValidationResult("The meeting END time cannot be before or the same as the meeting START time");
            }

            return ValidationResult.Success;
        }
    }
}