using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodDeliveryAggregateApp
{
    class OrderDTO
    {
        public long OrderId { get; set; }
        public long RID { get; set; }
        public long OrderBy { get; set; }
        public string Status { get; set; }
        public DateTime OrderDate { get; set; }

    }
}
