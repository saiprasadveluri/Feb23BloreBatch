using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodDelivery1
{
    public class OrderListDTO
    {
        public long OLID { get; set; }
        public long OderId { get; set; }
        public long MenuId { get; set; }
        public int  QTY { get; set; }

    }
}
