using Apps.MemoQResources.DataSourceHandlers.Enum;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dictionaries;

namespace Apps.MemoQResources.Models.Request
{
    public class ListTermBaseRequest
    {
        [Display("Languages")]
        [StaticDataSource(typeof(TargetLanguageDataHandler))]
        public IEnumerable<string>? Languages { get; set; }

        [Display("Name contains")]
        public string? NameContains { get; set; }

        [Display("Exact name")]
        public string? ExactName { get; set; }
    }
}
