using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DisconnetedModel
{
    public class Project
    {
        public long ProjectId { get; set; }
        public string ProjectName { get; set; }
        public long ProjectManagerId { get; set; }
        public string PStatus { get; set; }
    }
}
