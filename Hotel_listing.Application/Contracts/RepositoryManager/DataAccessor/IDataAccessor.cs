using Hotel_listing.Application.Common.RepositoryOptions;

namespace Hotel_listing.Application.Contracts.RepositoryManager.DataAccessor;

public interface IDataAccessor
{
    /// <summary>
    /// Get data form database using sql query or stored procedures
    /// </summary>
    /// <param name="options"></param>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TParams"></typeparam>
    /// <returns></returns>
    Task<IEnumerable<T>> Query<T,TParams>(DataAccessorOptions<TParams> options);

    /// <summary>
    /// Set data to the database using sql query or stored procedures
    /// </summary>
    /// <param name="options"></param>
    /// <typeparam name="TPramas"></typeparam>
    Task Command<TPramas>(DataAccessorOptions<TPramas> options);
}