using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskMangerADO
{
    public class UserDTO
    {
        public long UserId { get; set; }
        public string Name { get; set; }
        public string Department { get; set; }
        public long RoleId { get; set; }
        public long Email { get; set; }
        public long Password { get; set; }

    }
}
