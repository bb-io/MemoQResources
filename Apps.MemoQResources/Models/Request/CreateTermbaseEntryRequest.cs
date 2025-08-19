using Apps.MemoQResources.DataSourceHandlers.Enum;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dictionaries;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apps.MemoQResources.Models.Request
{
    public class CreateTermbaseEntryRequest
    {
        [Display("Termbase ID")]
        public string Guid { get; set; } = default!;

        public string? Client { get; set; }
        public string? Domain { get; set; }
        public string? Note { get; set; }
        public string? Project { get; set; }
        public string? Subject { get; set; }

        [Display("Language")]
        public string Language { get; set; } = default!;

        [Display("Text")]
        public string Text { get; set; } = default!;

        public string? Definition { get; set; }
        public string? Example { get; set; }

        [Display("Case sensitivity")]
        [StaticDataSource(typeof(CaseSensitivityLevelDataHandler))]
        public string? CaseSense { get; set; }

        [Display("Partial match")]
        [StaticDataSource(typeof(PartialMatchDegreeDataHandler))]
        public string? PartialMatch { get; set; }

        [Display("Forbidden")] 
        public bool? IsForbidden { get; set; }

        [Display("Needs moderation")] 
        public bool? NeedsModeration { get; set; }
    }
}
