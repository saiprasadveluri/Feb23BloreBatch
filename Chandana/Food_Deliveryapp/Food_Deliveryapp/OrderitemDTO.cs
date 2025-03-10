using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Food_Deliveryapp
{
    class OrderitemDTO
    {
        public long orderitemid { get; set; }
        public long oid { get; set; }
        public long mid { get; set; }   
        public long quantity { get; set; }
    }
}
