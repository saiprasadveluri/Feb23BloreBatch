using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagerADO
{
    public class ProjectDTO
    {
        public long Projectid { get; set; }
        public string Projecttitle { get; set; }
        public long PMid { get; set; }
        public string status { get; set; }
    }
}
