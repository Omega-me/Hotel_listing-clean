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