using System;
using System.Collections.Generic;

using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blackbird.Applications.Sdk.Common;

namespace Apps.MemoQResources.Models.Response
{
    public class UpdateTermResponse
    {
        [Display("Creation time")]
        public DateTime Created { get; set; }

        [Display("Creator")]
        public string Creator { get; set; }
        public DateTime Modified { get; set; }
        public string? Modifier { get; set; }
        public int Id { get; set; }
        public string? Client { get; set; }
        public string? Domain { get; set; }
        public string? ImageCaption { get; set; }
        public string? Image { get; set; }
        public string? Note { get; set; }
        public string? Project { get; set; }
        public string? Subject { get; set; }
        //public List<LanguageItem>? Languages { get; set; }
        //public List<TBCustomMetaItem>? CustomMetas { get; set; }
    }
}
