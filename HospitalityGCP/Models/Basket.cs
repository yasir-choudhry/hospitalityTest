using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace HospitalityGCP.Models
{
    public class Basket
    {
        [Key]
        public int BasketID { get; set; }

        public string Email { get; set; }

        public int MenuItemID { get; set; }

        [DisplayName("Name")]
        public string MenuItemName { get; set; }

        [DisplayName("Description")]
        public string MenuItemDesc { get; set; }

        [DisplayName("Quantity")]
        public int Quantity { get; set; }

        [DisplayName("Price")]
        public decimal MenuItemPrice { get; set; }

        [DisplayName("Delivery Time")]
        public TimeSpan ItemTime { get; set; }

        [DisplayName("Notice Required")]
        public string NoticeReq { get; set; }
    }
}
