﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagerADO
{
    class CommentDTO
    {
        public long CommentId { get; set; }
        public string Title { get; set; }
        public string CommentText { get; set; }
        public long TaskId { get; set; }
        public string CommentedBy { get; set; }
      
    }
}
