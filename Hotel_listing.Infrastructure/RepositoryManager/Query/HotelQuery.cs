using AutoMapper;
using Hotel_listing.Application.Contracts.RepositoryManager.DataAccessor;
using Hotel_listing.Application.Contracts.RepositoryManager.Query;
using Hotel_listing.Domain.Entitites;
using Hotel_listing.Persistence.Contexts;

namespace Hotel_listing.Infrastructure.RepositoryManager.Query;

public class HotelQuery: BaseQuery<Hotel>, IHotelQuery
{
    public HotelQuery(DatabaseContext context,IDataAccessor db, IMapper mapper) : base(context,db, mapper)
    { }
}