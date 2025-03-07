using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fooddeliveryapp.Data
{
    class OrderDTO
    {

        public long orderid { get; set; }
        public long restid { get; set; }
        public long userid { get; set; }
        public long itemid { get; set; }
        public string itemname { get; set; }
        public long qty { get; set; }
        public string status { get; set; }
        public decimal total { get; set; }
    }
}
