using X.PagedList;

namespace Hotel_listing.Application.Common.Features;
public class QueryReturn<T>
{
    public IPagedList<T> Results { get; set; }
    public int ResultsCount { get; set; }
}