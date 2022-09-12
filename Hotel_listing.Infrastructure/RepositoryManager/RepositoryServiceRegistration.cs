using Hotel_listing.Application.Contracts.RepositoryManager.Command;
using Hotel_listing.Application.Contracts.RepositoryManager.Query;
using Hotel_listing.Infrastructure.RepositoryManager.Command;
using Microsoft.Extensions.DependencyInjection;

namespace Hotel_listing.Infrastructure.RepositoryManager;

public static class RepositoryServiceRegistration
{
    public static void ConfigureRepository(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddTransient<IQuery,Query.Query>();
        serviceCollection.AddTransient<ICommands,Commands>();
    }
}