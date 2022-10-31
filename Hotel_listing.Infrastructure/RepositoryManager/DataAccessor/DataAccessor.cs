using Dapper;
using System.Data;
using Hotel_listing.Application.Common.Features;
using Microsoft.Extensions.Configuration;
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
    public async Task<IEnumerable<T>> Query<T,TParams>(DataAccessorOptions<TParams> options)
    {
        using IDbConnection connection = new NpgsqlConnection(_configuration.GetConnectionString(options.ConnectionId));
        return await connection.QueryAsync<T>(
            options.Sql,
            options.Prams,
            commandType : options.SqlType == Sqltype.Sql ? CommandType.Text : CommandType.StoredProcedure);
    }
    public async Task Command<TPramas>(DataAccessorOptions<TPramas> options)
    {
        using IDbConnection connection = new NpgsqlConnection(_configuration.GetConnectionString(options.ConnectionId));
        await connection.ExecuteAsync(
            options.Sql,
            options.Prams,
            commandType : options.SqlType == Sqltype.Sql ? CommandType.Text : CommandType.StoredProcedure);
    }
}