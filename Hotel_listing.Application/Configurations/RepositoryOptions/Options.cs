using System.Linq.Expressions;
using Microsoft.AspNetCore.Http;

namespace Hotel_listing.Application.Configurations.RepositoryOptions;

public class Options<T>
{
    public Expression<Func<T,bool>>? Expression { get; set; }
    public PaginationParams? Pagination { get; set; }
    public HttpContext? Context { get; set; }
    public Func<IQueryable<T>, IOrderedQueryable<T>> OrderBy { get; set; }
    public string? Filter { get; set; }
    public string? Sort { get; set; }
    public string? Includes { get; set; }
}