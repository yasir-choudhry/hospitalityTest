using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace HospitalityGCP.Models
{
    public class ViewModelOrdersList
    {
        [Key]
        [DisplayName("Order ID")]
        public int OrderID { get; set; }

        [DisplayName("Order Status")]
        public string OrderStatus { get; set; }

        [DisplayName("Date Order Placed")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime RequestedDateTime { get; set; }

        [DisplayName("Date Order Required")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime OrderDate { get; set; }

        [DisplayName("Ordered by Name")]
        public string UserName { get; set; }

        [DisplayName("Email")]

        public string UserEmail { get; set; }
        [DisplayName("Number of Attendees")]
        public string NumAttendees { get; set; }

        [DisplayName("Phone number")]
        public string UserPhone { get; set; }

        [DisplayName("Host's Name")]
        public string HostName { get; set; }

        [DisplayName("Host's Email")]
        public string HostEmail { get; set; }

        [DisplayName("Host's Phone")]
        public string HostPhone { get; set; }

        [DisplayName("Cost Centre")]
        public string CostCentre { get; set; }

        [DisplayName("Room / Location")]
        public string RoomDesc { get; set; }

        [DisplayName("Dietary Requirements")]
        public string DietaryReq { get; set; }

        [DisplayName("Notes")]
        public string Notes { get; set; }

        [DisplayName("Meeting start time")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:hh\\:mm}")]
        public TimeSpan MeetingStartTime { get; set; }

        [DisplayName("Meeting end time")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:hh\\:mm}")]
        public TimeSpan MeetingEndTime { get; set; }

        [DisplayName("Order Total")]
        public decimal? BasketTotal { get; set; }

        public static implicit operator ViewModelOrdersList(List<ViewModelOrdersList> v)
        {
            throw new NotImplementedException();
        }
    }
}
