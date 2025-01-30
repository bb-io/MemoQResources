using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blackbird.Applications.Sdk.Common.Dictionaries;

namespace Apps.MemoQResources.DataSourceHandlers.Enum
{
    public class CaseSensitivityLevelDataHandler : IStaticDataSourceHandler
    {
        protected Dictionary<string, string> EnumValues => new()
        {
            {"0", "Strict"},
            {"1", "Near"},
            {"2", "Insensitive"}
        };

        public Dictionary<string, string> GetData()
        {
            return EnumValues;
        }
    }
}
