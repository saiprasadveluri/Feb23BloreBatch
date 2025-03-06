using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagerADO
{
    public class CommentsDTO
    {
        public long Commentid { get; set; }
        public string Title { get; set; }
        public string Commenttext { get; set; }
        public long Taskid { get; set; }
        public long Commentedby { get; set; }

    }
}
