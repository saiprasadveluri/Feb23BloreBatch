using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagerADO
{
    public class TaskDTO
    {
      public long Taskid { get; set; }
      public string title { get; set; }
      public long Tasktype{ get; set; }
      public long Projectid { get; set; }
      public long assignedto { get; set; }
    }
}
