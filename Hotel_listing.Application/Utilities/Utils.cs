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

        public static List<string> QueryFilterTransformer(string queryFilter)
        {
            string valuePattern = @"(?<=\[).+?(?=\])";
            Regex valueRg = new Regex(valuePattern);
            MatchCollection valueMatches = valueRg.Matches(queryFilter);
            var incrementer = 0;
            var replacedQueryFilter = valueRg.Replace(queryFilter, match => $"@{(incrementer++).ToString()}");
            
            string filter =  replacedQueryFilter
                    .Replace("(=)", "==")
                    .Replace("(!=)", "!=")
                    .Replace("(>)", ">")
                    .Replace("(>=)", ">=")
                    .Replace("(<)", "<")
                    .Replace("(<=)", "<=")
                    .Replace("[","")
                    .Replace("]","")
                    .Replace("{and}", " and ")
                    .Replace("{or}", " or ")
                    .Replace("{&&}"," and ")
                    .Replace("{||}"," or ")
                ;
            List<string> listFilter = new List<string> {filter};
            for (int i = 0; i < valueMatches.Count; i++)
            {
                listFilter.Add(valueMatches[i].Value);
            }
            return listFilter;
        }

        public static string QuerySortTransformer(string querySort)
        {
            string sortQuery = querySort.Replace("_desc"," desc");
            return sortQuery;
        }
    }
}