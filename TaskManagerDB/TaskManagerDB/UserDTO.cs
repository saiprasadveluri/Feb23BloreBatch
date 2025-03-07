using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagerDB
{
    public class UserDTO
    {
        public long UserId { get; set; }
        public string Name { get; set; }
        public string Dept { get; set; }
        public long RoleID { get; set; }
    }
}
