using X.PagedList;

namespace Hotel_listing.Application.Common.RepositoryOptions;

public class QueryReturn<T>
{
    public IPagedList<T> Results { get; set; }
    public int ResultsCount { get; set; }
}