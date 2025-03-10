using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodDeliveryAggregateApp
{
    class RestaurantDTO
    {
        public long RID { get; set; }
        public string Name { get; set; }
        public long OwnerId { get; set; }
        public string Location { get; set; }
    }
}
