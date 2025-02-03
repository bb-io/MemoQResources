using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apps.MemoQResources.Polling.Models
{
    public class TermbaseMemory
    {
        public DateTime? LastPollingTime { get; set; }
        public bool Triggered { get; set; }
    }
}
