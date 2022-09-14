using Hotel_listing.Application.Contracts.RepositoryManager.Query;
using Hotel_listing.Domain.Entitites;
using Hotel_listing.Persistence.Context;

namespace Hotel_listing.Infrastructure.RepositoryManager.Query;

public class CountryQuery:BaseQuery<Country>, ICountryQuery
{
    public CountryQuery(DatabaseContext context) : base(context)
    { }
}