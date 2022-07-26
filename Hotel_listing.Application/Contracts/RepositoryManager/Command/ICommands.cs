namespace Hotel_listing.Application.Contracts.RepositoryManager.Command;

public interface ICommands : IDisposable
{
    ICountryCommand Countries { get; }
    IHotelCommand Hotels { get; }
    Task Save();
}