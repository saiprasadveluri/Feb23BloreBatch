using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagerDB
{
    public class ProjectDTO
    {
        public long ProjID { get; set; }
        public string Title { get; set; }
        public long PM { get; set; }
        public string Status { get; set; }
    }
}
