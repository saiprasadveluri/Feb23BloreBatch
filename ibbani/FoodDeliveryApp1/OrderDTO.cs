using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodDeliveryApp1
{
    public class OrderDTO
    {
        public long OID { get; set; }
        public long RID { get; set; }
        public long OrderBy { get; set; }
        public long OrderDate { get; set; }
        public long OrderStatus { get; set; }
    }
}