using Dapper;
using System.Data;
using Microsoft.Extensions.Configuration;
using Hotel_listing.Application.Common.RepositoryOptions;
using Hotel_listing.Application.Contracts.RepositoryManager.DataAccessor;
using Npgsql;

namespace Hotel_listing.Infrastructure.RepositoryManager.DataAccessor;

public class DataAccessor : IDataAccessor
{
    private readonly IConfiguration _configuration;

    public DataAccessor(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    /// <summary>
    /// Get data form database using sql query or stored procedures
    /// </summary>
    /// <param name="options"></param>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TParams"></typeparam>
    /// <returns></returns>
    public async Task<IEnumerable<T>> Query<T,TParams>(DataAccessorOptions<TParams> options)
    {
        using IDbConnection connection = new NpgsqlConnection(_configuration.GetConnectionString(options.ConnectionId));
        return await connection.QueryAsync<T>(
            options.Sql,
            options.Prams,
            commandType : options.SqlType == Sqltype.Sql ? CommandType.Text : CommandType.StoredProcedure);
    }

    /// <summary>
    /// Set data to the database using sql query or stored procedures
    /// </summary>
    /// <param name="options"></param>
    /// <typeparam name="TPramas"></typeparam>
    public async Task Command<TPramas>(DataAccessorOptions<TPramas> options)
    {
        using IDbConnection connection = new NpgsqlConnection(_configuration.GetConnectionString(options.ConnectionId));

        await connection.ExecuteAsync(
            options.Sql,
            options.Prams,
            commandType : options.SqlType == Sqltype.Sql ? CommandType.Text : CommandType.StoredProcedure);
    }
}