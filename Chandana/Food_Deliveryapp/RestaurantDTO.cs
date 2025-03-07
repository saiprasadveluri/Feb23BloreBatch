using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Food_Deliveryapp
{
    public class RestaurantDTO
    {
        public long rid { get; set; }
        public string rname { get; set; }
        public string location{ get; set; }
        public long ownerid { get; set; }
    }
}
