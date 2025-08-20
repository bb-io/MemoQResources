using Apps.MemoQResources.DataSourceHandlers;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dynamic;

namespace Apps.MemoQResources.Models.Request
{
    public class DeleteTermbaseEntryRequest
    {
        [Display("Termbase ID")]
        [DataSource(typeof(TermbaseDataHandler))]
        public string Guid { get; set; } = default!;

        [Display("Entry ID")]
        public string EntryId { get; set; }
    }
}
