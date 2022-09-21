using Hotel_listing.Application.Contracts.RepositoryManager.Command;
using Hotel_listing.Domain.Entitites;
using Hotel_listing.Persistence;
using DatabaseContext = Hotel_listing.Persistence.Contexts.DatabaseContext;

namespace Hotel_listing.Infrastructure.RepositoryManager.Command;

public class CountryCommand : BaseCommand<Country>, ICountryCommand
{
    public CountryCommand(DatabaseContext context) : base(context)
    { }
}