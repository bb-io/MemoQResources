using Apps.MemoQResources.Models.Items;
using Blackbird.Applications.Sdk.Common;

namespace Apps.MemoQResources.Models.Response
{
    public class UpdateTermResponse
    {
        [Display("Updated entry")]
        public TermbaseEntryResponse UpdatedEntry { get; set; }
    }
}
