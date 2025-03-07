using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Food_Deliveryapp
{
    public class OrderDTO
    {
        public long oid { get; set; }
        public long mid { get; set; }
        public long rid { get; set; }
        public long uid { get; set; }
        public long total { get; set; }
        public string status { get; set; }
        public string orderdate { get; set; }

    }
}
