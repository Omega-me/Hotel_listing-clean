using System.Linq.Expressions;

namespace Hotel_listing.Application.Contracts.RepositoryManager.Query;

public interface IBaseQuery<T> where T :class
{
    Task<List<T>> GetAll(
        Expression<Func<T,bool>>? expression=null,
        Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy=null,
        List<string>? includes=null);
    Task<T> Get(Expression<Func<T, bool>> expression, List<string>? includes = null);
}