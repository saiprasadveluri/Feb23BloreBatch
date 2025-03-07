using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagerADO
{
    class ProDTO
    {
        public long ProjectId { get; set; }
        public long PMID { get; set; }
        public string PTITLE { get; set; }     
        public string PSTATUS { get; set; }
    }
}
