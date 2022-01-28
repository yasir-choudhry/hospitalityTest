using System;
using System.ComponentModel.DataAnnotations;

namespace HospitalityGCP.Models
{
    public class OrderedItems
    { 

        [Key]
        public int OrderID { get; set; }

        public int MenuItemID { get; set; }

        public int Qty { get; set; }

        public decimal MenuItemPrice { get; set; }

        public TimeSpan ItemTime { get; set; }
    }
}
