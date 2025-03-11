using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodDelApp.DTOs
{
    public class RestaurantDTO
    {
        public int RID { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public int OwnerId { get; set; }
    }
}
