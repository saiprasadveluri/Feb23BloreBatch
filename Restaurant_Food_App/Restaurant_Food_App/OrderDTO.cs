using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant_Food_App
{
    public class OrderDTO
    {
        public long order_id { get; set; }
        public long user_id { get; set; }
        public long restaurant_id { get; set; }
        public string status { get; set; }
    }
}
