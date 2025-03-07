using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskMangerADO
{
    public class ProjectDTO
    {
        public long ProjId { get; set; } 
        public string title { get; set; }
        public long PM { get; set; }
        public string status { get; set; }
        }
    }

