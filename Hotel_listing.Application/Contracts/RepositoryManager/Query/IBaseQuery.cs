using System.Linq.Expressions;
using Hotel_listing.Application.Common.Features;
using Hotel_listing.Application.Common.RepositoryOptions;

namespace Hotel_listing.Application.Contracts.RepositoryManager.Query;

public interface IBaseQuery<T> where T :class
{
    Task<QueryReturn<T>> GetAll(Features<T> options);
    Task<T> Get(Expression<Func<T, bool>> expression, string? includes = null);
}