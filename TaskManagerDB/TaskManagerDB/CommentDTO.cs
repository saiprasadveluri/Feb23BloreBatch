using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagerDB
{
    public class CommentDTO
    {
        public long CommentID { get; set; }
        public string Title { get; set; }
        public string CommentText { get; set; }
        public long TaskID { get; set; }
        public long CommentedBy { get; set; }
    }
}
