using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fooddeliveryapp.Data
{
    public class MenuItemDTO
    {
        public long itemid { get; set; }
        public string itemname { get; set; }
        public string ingredients { get; set; }
        public long restid { get; set; }
        public decimal price { get; set; }

    }
}
