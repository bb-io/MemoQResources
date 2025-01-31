using Apps.MemoQResources.DataSourceHandlers;
using Apps.MemoQResources.DataSourceHandlers.Enum;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dictionaries;
using Blackbird.Applications.Sdk.Common.Dynamic;

namespace Apps.MemoQResources.Models.Request
{
    public class UpdateTermRequest
    {
        [Display("Termbase ID")]
        [DataSource(typeof(TermbaseDataHandler))]
        public string Guid { get; set; }

        [Display("Entry ID")]
        public string EntryId { get; set; }

        [Display("Client", Description = "Client of termbase entry")]
        public string? Client { get; set; }

        [Display("Domain", Description = "Domain of termbase entry")]
        public string? Domain { get; set; }

        [Display("Term definition")]
        public string? Definition { get; set; }

        [Display("Target language")]
        [StaticDataSource(typeof(TargetLanguageDataHandler))]
        public string Language { get; set; }

        [Display("Text of the term")]
        public string Text { get; set; }

        [Display("Case sensitivity", Description = "Defines the case sensitivity level of the term")]
        [StaticDataSource(typeof(CaseSensitivityLevelDataHandler))]
        public int? CaseSense { get; set; }

        [Display("Is forbidden")]
        public bool? TermIsForbidden { get; set; }

        [Display("Needs moderation")]
        public bool? Moderation { get; set; }

        [Display("Partial matches", Description = "Defines the degree to which partial matches are allowed against this term.")]
        [StaticDataSource(typeof(PartialMatchDegreeDataHandler))]
        public int? TermPartialMatches { get; set; }

        [Display("Example of term")]
        public string? Example { get; set; }

        [Display("Note", Description = "Note of termbase entry")]
        public string? Note { get; set; }

        [Display("Project", Description = "Project of termbase entry")]
        public string? Project { get; set; }

        [Display("Subject", Description = "Subject of termbase entry")]
        public string? Subject { get; set; }
    }
}
