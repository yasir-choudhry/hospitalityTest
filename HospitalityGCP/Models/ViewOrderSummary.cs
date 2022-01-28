using System.Collections.Generic;

namespace HospitalityGCP.Models
{
    public class ViewOrderSummary
    {
        public List<Order> Order { get; set; }

        public List<OrderedItems> OrderedItems { get; set; }
    }
}