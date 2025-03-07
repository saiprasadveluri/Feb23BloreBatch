using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodDeliveryApp
{
    public class OrderlistDTO
    {
        public long Olid {  get; set; }

        public long Orderid { get; set; }

        public long Menuid { get; set; }

        public int Qty { get; set; }
    }
}
