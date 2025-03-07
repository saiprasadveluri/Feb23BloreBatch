using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodDeliveryApp
{
    public class ResturantDTO
    {
        public long Rid { get; set; }

        public string Name { get; set; }

        public string Location { get; set; }

        public long Ownerid{ get; set; }
    }
}
