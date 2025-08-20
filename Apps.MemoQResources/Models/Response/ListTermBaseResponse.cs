using Blackbird.Applications.Sdk.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apps.MemoQResources.Models.Response
{
    public class ListTermBaseResponse
    {
        public IEnumerable<TermBaseListItem> Termbases { get; set; } = Array.Empty<TermBaseListItem>();
    }

    public class FindTBByNameResponse
    {
        public string? TBGuid { get; set; }
        public string? FriendlyName { get; set; }
        public IEnumerable<TermBaseListItem>? Matches { get; set; }
        public bool ExactMatch { get; set; }
    }

    public class TermBaseListItem
    {
        [Display("Access level")]
        public int AccessLevel { get; set; }
        public string? Client { get; set; }
        public string? Domain { get; set; }

        [Display("Friendly name")]
        public string? FriendlyName { get; set; }
        public string[]? Languages { get; set; }

        [Display("Last modified")]
        public DateTime? LastModified { get; set; }

        [Display("Last used")]
        public DateTime? LastUsed { get; set; }

        [Display("Num entries")]
        public int? NumEntries { get; set; }
        public string? Project { get; set; }
        public string? Subject { get; set; }

        [Display("Termbase ID")]
        public string? TBGuid { get; set; }

        [Display("Termbase owner")]
        public string? TBOwner { get; set; }

        [Display("Is used in project")]
        public bool? IsUsedInProject { get; set; }
    }
}
