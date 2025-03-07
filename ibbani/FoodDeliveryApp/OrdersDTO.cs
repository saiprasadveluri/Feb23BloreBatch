using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodDeliveryApp
{
    class OrdersDTO
    {
        public long orderid { get; set; }
        public string status { get; set; }
        public string orderDate { get; set; }
        public long orderedBy { get; set; }
        public long RID { get; set; }
    }
}
