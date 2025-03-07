using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagerApp
{
    public class TaskDTO
    {
        public long TaskId { get; set; }
        public string Title { get; set; }
        public string Type { get; set; } // Bug/Feature
        public long AssignedTo { get; set; }
        public long ProjID { get; set; }
        public string Status { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public float HoursLogged { get; set; }
    }

}
