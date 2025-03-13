using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodDelApp.Data
{
    public class OrderLineData
    {
        public long MenuId { get; set; }
        public int Qty { get; set; } 
        public string MenuName { get; set; }
        public double UnitTotal { get; set; }
    }
}
