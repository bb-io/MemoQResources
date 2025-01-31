using Apps.MemoQResources.DataSourceHandlers;
using Apps.MemoQResources.DataSourceHandlers.Enum;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dictionaries;
using Blackbird.Applications.Sdk.Common.Dynamic;

namespace Apps.MemoQResources.Models.Request
{
    public class UpdateTermRequest
    {
        [Display("Termbase GUID")]
        [DataSource(typeof(TermbaseDataHandler))]
        public string Guid { get; set; }

        [Display("Entry ID")]
        public string EntryId { get; set; }

        //Check the dispay names
        //make just for one language

        [Display("Client")]
        public string? Client { get; set; }

        [Display("Domain")]
        public string? Domain { get; set; }

        [Display("Language definition")]
        public string? Definition { get; set; }

        [Display("Language")]
        [StaticDataSource(typeof(TargetLanguageDataHandler))]
        public string Language { get; set; }

        [Display("Text")]
        public string Text { get; set; }

        [Display("Case sensitivity")]
        [StaticDataSource(typeof(CaseSensitivityLevelDataHandler))]
        public int? CaseSense { get; set; }

        [Display("Is forbidden")]
        public bool? TermIsForbidden { get; set; }

        [Display("Needs moderation")]
        public bool? Moderation { get; set; }

        [Display("Partial match")]
        [StaticDataSource(typeof(PartialMatchDegreeDataHandler))]
        public int? TermPartialMatches { get; set; }

        [Display("Example sentence")]
        public string? Example { get; set; }

        [Display("Note")]
        public string? Note { get; set; }

        [Display("Project")]
        public string? Project { get; set; }

        [Display("Subject")]
        public string? Subject { get; set; }
    }
}
