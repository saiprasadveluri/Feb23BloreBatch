using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodDeliveryApp
{
    public class MenuDTO
    {
       public long MenuId { get; set; }

        public string Menuname { get; set; }

        public long Rid { get; set; }

        public decimal UnitPrice { get; set; }

        public string FoodType { get; set; }
    }
}
