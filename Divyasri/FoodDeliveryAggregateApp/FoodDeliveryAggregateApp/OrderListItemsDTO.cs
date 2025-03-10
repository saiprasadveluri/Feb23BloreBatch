using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodDeliveryAggregateApp
{
    class OrderListItemsDTO
    {
        public long OLID { get; set; }
        public long OrderID { get; set; }
        public long MenuID { get; set; }
        public int Quantity { get; set; }
    }
}
