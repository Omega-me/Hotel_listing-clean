using Hotel_listing.Application.Contracts.RepositoryManager.Command;
using Hotel_listing.Domain.Entitites;
using Hotel_listing.Persistence;

namespace Hotel_listing.Infrastructure.RepositoryManager.Command;

public class CountryCommand : BaseCommand<Country>, ICountryCommand
{
    public CountryCommand(DatabaseContext context) : base(context)
    { }
}