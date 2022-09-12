using Hotel_listing.Infrastructure.DatabaseManager.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Hotel_listing.Infrastructure.DatabaseManager;

public static class DatabaseServiceRegistration
{
    public static void ConfigureDbContext(this IServiceCollection serviceCollection,IConfiguration configuration)
    {
        serviceCollection.AddDbContext<DatabaseContext>(
            options=>options.UseNpgsql(
                configuration.GetConnectionString("sqlConnectionPsql")
            )
        );
    }
}