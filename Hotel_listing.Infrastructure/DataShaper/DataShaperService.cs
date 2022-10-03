using Hotel_listing.Application.Contracts.DataShaper;
using Hotel_listing.Domain.Entitites;
using Microsoft.Extensions.DependencyInjection;

namespace Hotel_listing.Infrastructure.DataShaper;

public static class DataShaperService
{
    public static void ConfigureRepository(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<IDataShaper<Country>, DataShaper<Country>>();
    }
}