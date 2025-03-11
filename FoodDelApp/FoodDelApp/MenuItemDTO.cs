using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodDelApp.DTOs
{
    public class MenuItemDTO
    {
        public int MID { get; set; }
        public string MenuName { get; set; }
        public int UnitPrice { get; set; }
        public string FoodType { get; set; }
        public int RID { get; set; }
    }
}