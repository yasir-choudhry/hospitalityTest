using System.Collections.Generic;

namespace HospitalityGCP.Models
{
    public class ViewModelShowBasket
    {
        public List<MenuItems> MenuItems { get; set; }

        public List<Basket> ShowItems { get; set; }
    }
}
