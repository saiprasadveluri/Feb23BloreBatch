using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodDeliveryApp1
{
    public class RestaurantDTO
    {
        public long RID { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public long OwnerId { get; set; }

    }
}
