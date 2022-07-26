using Hotel_listing.Application.Contracts.RepositoryManager.Query;
using Hotel_listing.Infrastructure.DatabaseManager.Context;

namespace Hotel_listing.Infrastructure.RepositoryManager.Query;

public class Query:IQuery
{
    private readonly DatabaseContext _context;
        
    private ICountryQuery _countries;
    private IHotelQuery _hotels;

    public Query(DatabaseContext context) {
        _context = context;
    }
        
    public ICountryQuery Countries => _countries ??= new CountryQuery(_context);
    public IHotelQuery Hotels => _hotels ??= new HotelQuery(_context);


    public void Dispose() {
        _context.Dispose();
        GC.SuppressFinalize(this);
    }


    public async Task Save() {
        await _context.SaveChangesAsync();
    }
}