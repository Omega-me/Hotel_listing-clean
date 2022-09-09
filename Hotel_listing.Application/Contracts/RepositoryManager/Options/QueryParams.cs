using System.Linq.Expressions;

namespace Hotel_listing.Application.Contracts.RepositoryManager.Options;

public class QueryParams<T>
{
    public Expression<Func<T,bool>>? Expression { get; set; }
    public string? Sort { get; set; }
    public string? Includes { get; set; }
    public PaginationParams? Pagination { get; set; }
}