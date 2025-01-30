using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apps.MemoQResources.Polling.Models
{
    public class TermbaseUpdateResponse
    {
        public int AccessLevel { get; set; }
        public string Client { get; set; } = string.Empty;
        public string Domain { get; set; } = string.Empty;
        public string FriendlyName { get; set; } = string.Empty;
        public List<string> Languages { get; set; } = new();
        public DateTime LastModified { get; set; }
        public int NumEntries { get; set; }
        public string Project { get; set; } = string.Empty;
        public string Subject { get; set; } = string.Empty;
        public string TBGuid { get; set; }
        public string TBOwner { get; set; } = string.Empty;
        public DateTime LastUsed { get; set; }
        public bool IsUsedInProject { get; set; }
    }
}
