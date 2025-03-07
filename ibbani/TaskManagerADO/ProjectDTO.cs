using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace TaskManagerADO
{
    
    public class ProjectDTO
    {
        public long ProjId { get; set; }
        public string Title { get; set; }
        public long ProjManager { get; set; }
        public string Status { get; set; }
    }
}
