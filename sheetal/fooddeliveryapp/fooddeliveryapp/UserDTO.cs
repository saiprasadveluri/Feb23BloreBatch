using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fooddeliveryapp.Data
{
    public class UserDTO
    {
        public long userid { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string role { get; set; }
        public string address { get; set; }
        public string preferences { get; set; }



    }
}

