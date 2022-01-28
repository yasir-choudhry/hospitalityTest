using System.Collections.Generic;

namespace HospitalityGCP.Models
{
    public class ViewModelOrderDetails
    {
        public ViewModelOrdersList OrdersDetails { get; set; }
        public List<ViewModelOrderedItems> OrderedItems { get; set; }
    }
}
