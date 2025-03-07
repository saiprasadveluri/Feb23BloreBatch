using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant_Food_App
{
    public class Order_item
    {
       public long order_item { get; set; }
        public long order_id { get; set; }
        public long menu_id { get; set; }
           public int quantity { get; set; }
    }
}
