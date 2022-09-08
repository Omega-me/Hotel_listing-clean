using System.Linq.Expressions;

namespace Hotel_listing.Application.Contracts.RepositoryManager.Options;

public partial class QueryOptions<T>
{
    public Expression<Func<T,bool>>? Expression { get; set; }
    public Func<IQueryable<T>, IOrderedQueryable<T>>? OrderBy { get; set; }
    public List<string>? Includes { get; set; }
    public PaginationOptions? Pagination { get; set; }
}