using Hotel_listing.Application.Contracts.DataShaper;
using Hotel_listing.Domain.Entitites;
using Microsoft.Extensions.DependencyInjection;

namespace Hotel_listing.Infrastructure.DataShaper;

public static partial class DataShaperService
{
    public static void ConfigureDataShaper(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<IDataShaper<Country>, DataShaper<Country>>();
        serviceCollection.AddScoped<IDataShaper<Hotel>, DataShaper<Hotel>>();
    }
}