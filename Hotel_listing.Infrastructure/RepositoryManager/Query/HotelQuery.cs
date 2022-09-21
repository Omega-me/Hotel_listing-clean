using Hotel_listing.Application.Contracts.RepositoryManager.Query;
using Hotel_listing.Domain.Entitites;
using DatabaseContext = Hotel_listing.Persistence.Contexts.DatabaseContext;

namespace Hotel_listing.Infrastructure.RepositoryManager.Query;

public class HotelQuery: BaseQuery<Hotel>, IHotelQuery
{
    public HotelQuery(DatabaseContext context) : base(context)
    { }
}