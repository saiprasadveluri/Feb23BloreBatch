using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fooddeliveryapp.Data
{
    public class RestaurantDTO
    {

        public long restaurantid { get; set; }
        public string rname { get; set; }
        public string location { get; set; }
        public long ownerid { get; set; }

    }
}
