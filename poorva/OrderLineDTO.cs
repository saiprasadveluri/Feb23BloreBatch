using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodDeliveryAggregateApp
{
    public class OrderLineDTO
    {
        public long OLID { get; set; }
        public long OrderID { get; set; }
        public long MenuItemId { get; set; }
        public int Qty { get; set; }
    }
}
