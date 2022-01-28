using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace HospitalityGCP.Models
{
    public class MenuItems
    {
        [Key]
        [DisplayName("ID")]
        public int MenuItemID { get; set; }

        [DisplayName("Position")]
        [Required]
        public int Position { get; set;}

        [DisplayName("Item")]
        [Required]
        public string MenuItemName { get; set; }

        [DisplayName("Description")]
        [Required]
        public string MenuItemDesc { get; set; }

        [DisplayName("Price")]
        [Required]
        public decimal MenuItemPrice { get; set; }

        [DisplayName("Notice Required")]
        [Required]
        public string NoticeReq { get; set; }

        [DisplayName("Remove from display")]
        [Required]
        public bool RFD { get; set; }
    }
}