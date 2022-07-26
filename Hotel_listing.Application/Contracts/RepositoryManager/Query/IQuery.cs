namespace Hotel_listing.Application.Contracts.RepositoryManager.Query;

public interface IQuery : IDisposable
{
    ICountryQuery Countries { get; }
    IHotelQuery Hotels { get; }
    Task Save();
}