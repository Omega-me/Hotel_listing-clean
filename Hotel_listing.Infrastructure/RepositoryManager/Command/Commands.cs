/// <summary>
///     This file is autogenerated by toolkit, next time you generate files again it wil be regenerated
///     If you want to do something custom with this class use partial instead
/// </summary>

using Hotel_listing.Application.Contracts.RepositoryManager.Command;
using Hotel_listing.Persistence;
using DatabaseContext = Hotel_listing.Persistence.Contexts.DatabaseContext;

namespace Hotel_listing.Infrastructure.RepositoryManager.Command;
public partial class Commands : ICommands
{
    private readonly DatabaseContext _context;
    ICountryCommand _Country { get;set; }
    IHotelCommand _Hotel { get;set; }
    public Commands(DatabaseContext context) {
        _context = context;
    }
    public ICountryCommand Country => _Country ??= new CountryCommand(_context);
    public IHotelCommand Hotel => _Hotel ??= new HotelCommand(_context);
     
     public void Dispose() {
        _context.Dispose();
        GC.SuppressFinalize(this);
    }
    public async Task Save() {
        await _context.SaveChangesAsync();
    }
}
