using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace HospitalityGCP.Models
{
    public class Rooms
    {
        [Key]
        public int RoomID { get; set; }

        [DisplayName("Room or breakout area")]
        [Required(ErrorMessage = "Please enter a room or breakout area description")]
        public string RoomDesc { get; set; }

        [DisplayName("Remove from display")]
        public bool RFD { get; set; }
    }
}
