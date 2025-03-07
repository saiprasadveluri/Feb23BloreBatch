using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodDeliveryApp
{
    public class RestaurentDTO
    {
        public long RID { get; set; }
		public string RestName { get; set; }
		public string location{ get; set; }

		public long Ownerid { get; set; }
	}
}
