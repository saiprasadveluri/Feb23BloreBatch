using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodDeliveryApp
{
    public class OrderDTO
    {
        public long Orderid { get; set; }

        public long Rid { get; set; }

        public long OrderBy { get; set; }

        public string Status { get; set; }

        public long OrderDate { get; set; }
    }
}
