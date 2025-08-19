using Apps.MemoQResources.Models.Items;
using Blackbird.Applications.Sdk.Common;

namespace Apps.MemoQResources.Models.Response
{
    public class CreateTermResponse
    {
        [Display("Created entry")]
        public TermbaseEntryResponse? CreatedEntry { get; set; }

        [Display("Raw response")]
        public string? RawResponse { get; set; }
    }
}
