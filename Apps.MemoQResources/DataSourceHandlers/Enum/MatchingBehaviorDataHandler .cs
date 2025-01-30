using Blackbird.Applications.Sdk.Common.Dictionaries;

namespace Apps.MemoQResources.DataSourceHandlers.Enum
{
    public class MatchingBehaviorDataHandler : IStaticDataSourceHandler
    {
        protected Dictionary<string, string> EnumValues => new()
        {
            {"0", "BeginsWith"},
            {"1", "Contains"},
            {"2", "EndsWith"},
            {"3", "ExactMatch"}
        };

        public Dictionary<string, string> GetData()
        {
            return EnumValues;
        }
    }
}
