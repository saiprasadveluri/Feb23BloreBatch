using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagerDB
{
    public class TaskDTO
    {
        public long TaskID { get; set; }
        public string Title { get; set; }
        public long TaskType { get; set; }
        public long ProjID { get; set; }
        public long AssignTo { get; set; }
        public string Status { get; set; } 
    }
}
