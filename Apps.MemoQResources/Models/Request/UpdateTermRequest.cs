using System;
using System.Collections.Generic;

using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        [Display("Desired matching behavior")]
        [StaticDataSource(typeof(MatchingBehaviorDataHandler))]
        public string Condition {  get; set; }

        [Display("Language")]
        [StaticDataSource(typeof(TargetLanguageDataHandler))]
        public string Language {  get; set; }

        [Display("Search expression")]
        public string SearchExpression { get; set; }

        [Display("Entry ID")]
        public string Entry { get; set; }

        [Display("Name")]
        public string Name  { get; set; }

        [Display("Custom meta name")]
        public string? MetaName { get; set; }

        [Display("Custom meta value")]
        public string? MetaValue { get; set; }

        public string? Definition {  get; set; }

        [Display("Case sensitivity")]
        [StaticDataSource(typeof(CaseSensitivityLevelDataHandler))]
        public int? CaseSense { get; set; }

        [Display("Is forbidden")]
        public bool? TermIsForbidden { get; set; }

        [Display("Needs moderation")]
        public bool? Moderation { get; set; }

        [Display("Partial match")]
        [StaticDataSource(typeof(PartialMatchDegreeDataHandler))]
        public int TermPartialMatches { get; set; }

        [Display("Modified")]
        public DateTime Modified {  get; set; }

        [Display("Example sentence")]
        public string? TermExample { get; set; } 

        [Display("Domain")]
        public string? Domain { get; set; }
    }
}
