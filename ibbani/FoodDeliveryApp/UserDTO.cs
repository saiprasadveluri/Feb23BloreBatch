using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodDeliveryApp
{
    public class UserDTO
    {
        public long userid { get; set; }
        public string username { get; set; }
        public string rolename { get; set; }
        public string address{ get; set; }
        public string email { get; set; }
        public string pswd { get; set; }
    }
}
