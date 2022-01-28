using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace HospitalityGCP.Models
{
    public class Order
    {
        [Key]
        public int OrderID { get; set; }

        public DateTime RequestedDateTime { get; set; }

        public int OrderStatusID { get; set; }

        [DisplayName("Name")]
        [Required(ErrorMessage = "Please enter a name")]
        public string UserName { get; set; }

        [DisplayName("Email")]
        [Required(ErrorMessage = "Please enter your Email address")]
        [EmailAddress]
        public string UserEmail { get; set; }

        [DisplayName("Phone number")]
        [Required(ErrorMessage = "Please enter your phone number")]
        public string UserPhone { get; set; }

        [HostName]
        [DisplayName("Host Name (if different)")]
        public string HostName { get; set; }

        [HostEmail]
        [DisplayName("Host Email (if different)")]
        [EmailAddress]
        public string HostEmail { get; set; }

        [HostPhone]
        [DisplayName("Host Phone Number (if different)")]
        public string HostPhone { get; set; }

        [DisplayName("Cost Centre")]
        [Required(ErrorMessage = "Please enter a cost centre")]
        public string CostCentre { get; set; }

        [DisplayName("Delivery Date")]
        [OrderDatePastFuture]
        [FortyEightHours]
        public DateTime OrderDate { get; set; }

        [DisplayName("Meeting start time")]
        [Required(ErrorMessage = "Please enter a start time")]
        public TimeSpan MeetingStartTime { get; set; }

        [DisplayName("Meeting end time")]
        [Required(ErrorMessage = "Please enter the finish time")]
        [MeetingEndTime]
        public TimeSpan MeetingEndTime { get; set; }

        [DisplayName("Number of Attendees")]
        [Required(ErrorMessage = "Please enter the number of attendees")]
        public string NumAttendees { get; set; }

        [DisplayName("Room / breakout area")]
        [RoomSelect]
        public int RoomID { get; set; }

        [DisplayName("Dietary Requirements")]
        public string DietaryReq { get; set; }

        public string Notes { get; set; }

        public decimal? BasketTotal { get; set; }
    }
}
