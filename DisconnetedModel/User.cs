using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DisconnetedModel
{
    public class User
    {
     
            public long UserId { get; set; }
            public string Name { get; set; }
            public string Dept { get; set; }
            public long RoleId { get; set; }
            public string Email { get; set; }
            public string Password { get; set; }
        
    }
}
