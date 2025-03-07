using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagerApp
{
    public class CommentDTO
    {
        public long CommentId { get; set; }
        public long TaskID { get; set; }
        public long CommentedBy { get; set; }
        public string CommentText { get; set; }
    }

}
