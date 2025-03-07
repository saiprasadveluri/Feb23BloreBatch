using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant_Food_App
{
    public class RestaurantDTO
    {
        
            public long restaurant_id { get; set; }
        public string res_name { get; set; }
        public string city { get; set; }
        public long phone { get; set; }
        public long owner_id { get; set; }

    }
}
