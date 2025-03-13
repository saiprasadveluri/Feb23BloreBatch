using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodDelApp.Data
{
    public class MenuItemDTO
    {
        public long MID {  get; set; }
        public string MenuName { get; set; }
        public long RID { get; set; }
        public double UnitPrice { get; set; }
        public string FoodType   { get; set; }
    }
}
