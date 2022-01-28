using System;
using System.ComponentModel;

namespace HospitalityGCP.Models
{
    public class Reports
    {
        [DisplayName("Start Date")]
        public DateTime StartDate { get; set; }

        [DisplayName("End Date")]
        public DateTime EndDate { get; set; }
    }
}
