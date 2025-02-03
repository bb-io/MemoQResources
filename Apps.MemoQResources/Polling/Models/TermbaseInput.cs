using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Apps.MemoQResources.DataSourceHandlers;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dynamic;

namespace Apps.MemoQResources.Polling.Models
{
    public class TermbaseInput
    {
        [Display("Termbase GUID")]
        [DataSource(typeof(TermbaseDataHandler))]
        public string TbId { get; set; }
    }
}
