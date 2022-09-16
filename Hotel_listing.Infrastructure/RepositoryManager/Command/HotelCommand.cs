using Hotel_listing.Application.Contracts.RepositoryManager.Command;
using Hotel_listing.Domain.Entitites;
using DatabaseContext = Hotel_listing.Persistence.DatabaseContext;

namespace Hotel_listing.Infrastructure.RepositoryManager.Command;

public class HotelCommand: BaseCommand<Hotel>, IHotelCommand
{
    public HotelCommand(DatabaseContext context) : base(context)
    { }
}