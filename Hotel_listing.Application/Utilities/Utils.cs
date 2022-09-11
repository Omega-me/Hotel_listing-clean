using System.Text.RegularExpressions;

namespace Hotel_listing.Application.Utilities
{
    public static class Utils
    {
        public static string GenerateBasePath(string directory)
        {
            var appPath = Environment.CurrentDirectory.Split("\\").SkipLast(1).ToArray();
            var path = String.Join("\\", appPath) + directory;
            return path;
        }

        public static string QueryFilterTransformer(string queryFilter)
        {
            string valuePattern = @"/\[(.*?)\]/g";
            // string logicPattern = @"/\{(.*?)\}/g";
            // string operatorPattern = @"/\((.*?)\)/g";
            // string valuePattern = @"/(?<=\[).+?(?=\])/g";
            // string logicPattern = @"/(?<=\{).+?(?=\})/g";
            // string operatorPattern = @"/(?<=\().+?(?=\))/g";
            // Regex valueRg = new Regex(valuePattern);
            // Regex logicRg = new Regex(logicPattern);
            // Regex operatorRg = new Regex(operatorPattern);
            // MatchCollection values = valueRg.Matches(queryFilter);
            // MatchCollection logics = logicRg.Matches(queryFilter);
            // MatchCollection operators = operatorRg.Matches(queryFilter);

            Match match = Regex.Match(queryFilter,valuePattern,RegexOptions.Compiled);
            if (match.Success)
            {
                var value = match.Value;
            }

            string filter =  queryFilter
                    .Replace("(=)", "==")
                    .Replace("(!=)", "!=")
                    .Replace("(>)", ">")
                    .Replace("(>=)", ">=")
                    .Replace("(<)", "<")
                    .Replace("(<=)", "<=")
                    .Replace("[","\"")
                    .Replace("]","\"")
                    .Replace("{and}", " and ")
                    .Replace("{or}", " or ")
                ;

            return filter;
        }

        public static string QuerySortTransformer(string querySort)
        {
            string sortQuery = querySort.Replace("_desc"," desc");
            return sortQuery;
        }
    }
}