using System;
using System.Collections.Generic;

namespace HospitalityGCP.Models
{
    public class ViewModelUserReport
    {
        public List<ViewModelOrdersList> OrdersList { get; set; }
        public DateTime? reportDate { get; set; }
    }
}
