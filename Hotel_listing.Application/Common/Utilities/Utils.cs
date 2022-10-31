using System.Text.RegularExpressions;

namespace Hotel_listing.Application.Common.Utilities;

public static class Utils
{
    public static string GenerateBasePath(string directory)
    {
        var appPath = Environment.CurrentDirectory.Split(@"\").SkipLast(1).ToArray();
        var path = String.Join("\\", appPath) + directory;
        return path;
    }

    public static List<dynamic> QueryFilterTransformer(string queryFilter)
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
            .Replace("(in)"," in ")
            .Replace("{and}", " and ")
            .Replace("{or}", " or ")
            .Replace("{&&}"," and ")
            .Replace("{||}"," or ")
            .Replace("[","")
            .Replace("]","");
        List<dynamic> listFilter = new List<dynamic> {filter};
        foreach (Match match in valueMatches)
        {
            if (match.Value.Contains("{") && match.Value.Contains("}"))
            {
                string[] inValues = match.Value.Replace("{", "").
                    Replace("}", "").
                    Split(",").
                    ToArray();
                listFilter.Add(inValues);
            }
            else
            {
                listFilter.Add(match.Value);                
            }
        }
        return listFilter;
    }
    public static string QuerySortTransformer(string querySort)
    {
        string sortQuery = querySort.Replace("_desc"," desc");
        return sortQuery;
    } 
}