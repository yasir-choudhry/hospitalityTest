using System;
using System.ComponentModel.DataAnnotations;

namespace HospitalityGCP.Models
{
    public class OrderDatePastFutureAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {


            Order dateOrder = (Order)validationContext.ObjectInstance;
            DateTime today = DateTime.Now;
            DateTime plusone = today.AddDays(1);

            Order futureOrder = (Order)validationContext.ObjectInstance;
            DateTime future = DateTime.Now;
            DateTime futuredate = future.AddMonths(6);

            if (dateOrder.OrderDate < plusone || futureOrder.OrderDate > futuredate)
            {
                return new ValidationResult("The delivery date cannot be in the past or more than 6 months in the future " +
                    "and has to be one full business day after the date ordered, so can't be tomorrow " +
                    "OR you have items in your basket that require two full business days notice, " +
                    "please amend your basket or change the delivery date");
            }

            return ValidationResult.Success;
        }
    }
}
