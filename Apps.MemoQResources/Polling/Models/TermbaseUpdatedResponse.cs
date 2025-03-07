using Blackbird.Applications.Sdk.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apps.MemoQResources.Polling.Models
{
    public class TermbaseUpdateResponse
    {
        [Display("Access level")]
        public int AccessLevel { get; set; }

        [Display("Client ID")]
        public string Client { get; set; } = string.Empty;

        [Display("Domain ID")]
        public string Domain { get; set; } = string.Empty;

        [Display("Name")]
        public string FriendlyName { get; set; } = string.Empty;

        [Display("Languages")]
        public List<string> Languages { get; set; } = new();

        [Display("Last modified")]
        public DateTime LastModified { get; set; }

        [Display("Number of entries")]
        public int NumEntries { get; set; }

        [Display("Project ID")]
        public string Project { get; set; } = string.Empty;

        [Display("Subject ID")]
        public string Subject { get; set; } = string.Empty;

        [Display("Termbase ID")]
        public string TBGuid { get; set; }

        [Display("Termbase Owner")]
        public string TBOwner { get; set; } = string.Empty;

        [Display("Last used")]
        public DateTime LastUsed { get; set; }

        [Display("Is used in project?")]
        public bool IsUsedInProject { get; set; }
    }
}
