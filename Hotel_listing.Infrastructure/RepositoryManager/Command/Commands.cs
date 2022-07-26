using Hotel_listing.Application.Contracts.RepositoryManager.Command;
using Hotel_listing.Infrastructure.DatabaseManager.Context;

namespace Hotel_listing.Infrastructure.RepositoryManager.Command;

public class Commands : ICommands
{
    private readonly DatabaseContext _context;
        
    private ICountryCommand _countries;
    private IHotelCommand _hotels;

    public Commands(DatabaseContext context) {
        _context = context;
    }
        
    public ICountryCommand Countries => _countries ??= new CountryCommand(_context);
    public IHotelCommand Hotels => _hotels ??= new HotelCommand(_context);


    public void Dispose() {
        _context.Dispose();
        GC.SuppressFinalize(this);
    }


    public async Task Save() {
        await _context.SaveChangesAsync();
    }
}
