using Blackbird.Applications.Sdk.Common.Dictionaries;

namespace Apps.MemoQResources.DataSourceHandlers.Enum
{
    public class PartialMatchDegreeDataHandler : IStaticDataSourceHandler
    {
        protected Dictionary<string, string> EnumValues => new()
        {
            ["0"]= "Any",
            ["1"]= "Half",
            ["2"] = "None",
            ["3"] = "Custom"
        };

        public Dictionary<string, string> GetData()
        {
            return EnumValues;
        }
    }
}
