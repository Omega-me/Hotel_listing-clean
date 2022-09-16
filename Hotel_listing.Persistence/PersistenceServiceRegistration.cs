using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Hotel_listing.Persistence;

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