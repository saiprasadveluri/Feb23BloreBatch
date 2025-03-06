using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DisconnetedModel
{
    public class Comments
    {
        public long CommentId { get; set; }
        public string CommentTitle { get; set; }
        public string CommentText { get; set; }
        public long TaskId { get; set; }
        public long CommentedBy { get; set; }

    }
}
