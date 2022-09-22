using Hotel_listing.Application.Contracts.RepositoryManager.Command;
using Hotel_listing.Application.Contracts.RepositoryManager.DataAccessor;
using Hotel_listing.Application.Contracts.RepositoryManager.Query;
using Hotel_listing.Infrastructure.RepositoryManager.Command;
using Hotel_listing.Infrastructure.RepositoryManager.DataAccessor;
using Hotel_listing.Infrastructure.RepositoryManager.Query;
using Microsoft.Extensions.DependencyInjection;

namespace Hotel_listing.Infrastructure.Extensions;

public static class InfrastructureServiceRegistration
{
    public static void ConfigureRepository(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddTransient<IQuery,Query>();
        serviceCollection.AddTransient<ICommands,Commands>();
        serviceCollection.AddTransient<IDataAccessor, DataAccessor>();
    }
}