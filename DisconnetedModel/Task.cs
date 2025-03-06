using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DisconnetedModel
{
    public class Task
    {
        public long TaskId { get; set; }
        public string TaskTitle { get; set; }
        public long TaskType { get; set; }
        public long ProjId { get; set; }
        public long AssignedTo { get; set;}
    }
}
