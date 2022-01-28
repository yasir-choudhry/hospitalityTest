using HospitalityGCP.Data;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace HospitalityGCP.Models
{
    public class FortyEightHoursAttribute : ValidationAttribute
    {

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var _context = (HospitalityGCPContext)validationContext.GetService(typeof(HospitalityGCPContext));

            Order dateOrder = (Order)validationContext.ObjectInstance;
            DateTime today = DateTime.Now;
            DateTime plustwo = today.AddDays(2);

            var usersBasket = _context.Basket.Where(m => m.Email == SystemUser.GetCurrentUserEmail() && m.NoticeReq == "Two full business days").ToList();

            if (usersBasket.Count > 0)
            {
                if (dateOrder.OrderDate < plustwo)
                {
                    return new ValidationResult("The delivery date cannot be in the past or more than 6 months in the future " +
                    "and has to be one full business day after the date ordered, so can't be tomorrow " +
                    "OR you have items in your basket that require two full business days notice, " +
                    "please amend your basket or change the delivery date");
                }
            }
            return ValidationResult.Success;
        }
    }
}
