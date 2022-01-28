using System;
using System.Collections.Generic;

namespace HospitalityGCP.Models
{
    public class ViewModelCatererReport
    {
        public List<ViewModelOrdersList> OrdersList { get; set; }
        public DateTime? startDate { get; set; }
        public DateTime? endDate { get; set; }
    }
}
