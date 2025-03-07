using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant_Food_App
{
    public class MenuDTO
    {
        public long menu_id { get; set; }
        public long restaurant_id { get; set; }
        public string menu_item { get; set; }
        public long UnitPrice { get; set; }
        public string Foodtype { get; set; }
       
    }
}
