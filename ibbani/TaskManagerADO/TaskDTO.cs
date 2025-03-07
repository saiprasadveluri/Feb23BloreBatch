using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace TaskManagerADO
{
   
    public class TaskDTO
    {
        public long TaskId { get; set; }
        public string Title { get; set; }
        public long TaskType { get; set; }
        public long ProjId { get; set; }
        public long AssignedTo { get; set; }
    }

}