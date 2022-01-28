using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace HospitalityGCP.Models
{
    public class ViewModelOrderedItems
    {
        [Key]
        public int OrderNum { get; set; }

        [DisplayName("Order ID")]
        public int OrderID { get; set; }

        [DisplayName("Item")]
        [Required]
        public string MenuItemName { get; set; }

        [DisplayName("Description")]
        [Required]
        public string MenuItemDesc { get; set; }

        [DisplayName("Notice required")]
        [Required]
        public string NoticeReq { get; set; }

        [DisplayName("Quantity")]
        [Required]
        public int Qty { get; set; }

        [DisplayName("Price")]
        [Required]
        public decimal MenuItemPrice { get; set; }

        [DisplayName("Time required")]
        [Required]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:hh\\:mm}")]
        public TimeSpan ItemTime { get; set; }
    }
}