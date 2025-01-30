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
        public int EntryId { get; set; }

        [Display("Search expression")]
        public string SearchExpression { get; set; }

        [Display("Client")]
        public string? Client { get; set; }

        [Display("Domain")]
        public string? Domain { get; set; }

        [Display("Language definition")]
        public string? Definition { get; set; }

        [Display("Desired matching behavior")]
        [StaticDataSource(typeof(MatchingBehaviorDataHandler))]
        public int Condition { get; set; }

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

        [Display("Modified date")]
        public DateTime Modified { get; set; }

        [Display("Example sentence")]
        public string? TermExample { get; set; }

        [Display("Note")]
        public string? Note { get; set; }

        [Display("Project")]
        public string? Project { get; set; }

        [Display("Subject")]
        public string? Subject { get; set; }
    }
}
