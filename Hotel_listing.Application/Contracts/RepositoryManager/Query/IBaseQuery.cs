using System.Linq.Expressions;
using Hotel_listing.Application.Contracts.RepositoryManager.Options;

namespace Hotel_listing.Application.Contracts.RepositoryManager.Query;

public interface IBaseQuery<T> where T :class
{
    Task<List<T>> GetAll(Options<T> options);
    Task<T> Get(Expression<Func<T, bool>> expression, List<string>? includes = null);
}