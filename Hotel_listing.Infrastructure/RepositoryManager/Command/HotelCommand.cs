using AutoMapper;
using Hotel_listing.Application.Contracts.RepositoryManager.Command;
using Hotel_listing.Application.Contracts.RepositoryManager.DataAccessor;
using Hotel_listing.Domain.Entitites;
using Hotel_listing.Persistence.Contexts;

namespace Hotel_listing.Infrastructure.RepositoryManager.Command;

public class HotelCommand: BaseCommand<Hotel>, IHotelCommand
{
    public HotelCommand(DatabaseContext context,IDataAccessor db,IMapper mapper) : base(context,db,mapper)
    { }
}