using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagerApp
{
    public class ProjectDTO
    {
        public long ProjID { get; set; }
        public string Title { get; set; }
        public string Status { get; set; }
        public long ManagerId { get; set; }
    }

}
