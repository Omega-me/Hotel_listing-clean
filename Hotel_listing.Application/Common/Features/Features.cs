using System.Linq.Expressions;

namespace Hotel_listing.Application.Common.Features;
public class Features<T>
{
    public Expression<Func<T,bool>>? Expression { get; set; }
    public PaginationParams? Pagination { get; set; }
    public Func<IQueryable<T>, IOrderedQueryable<T>> OrderBy { get; set; }
    public string? Filter { get; set; }
    public string? Sort { get; set; }
    public string? Includes { get; set; }
    public string? Fields { get; set; } 
}