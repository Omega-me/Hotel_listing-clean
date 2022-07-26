/// <summary>
///     This file is autogenerated by toolkit, next time you generate files again it wil be regenerated
///     If you want to do something custom with this class use partial instead
/// </summary>

using AutoMapper;
using Hotel_listing.Application.Contracts.RepositoryManager.Command;
using Hotel_listing.Application.Contracts.RepositoryManager.DataAccessor;
using Hotel_listing.Persistence.Contexts;

namespace Hotel_listing.Infrastructure.RepositoryManager.Command;
public partial class Commands : ICommands
{
    private readonly DatabaseContext _context;
    private readonly IDataAccessor _db;
    private readonly IMapper _mapper;
    ICountryCommand _Country { get;set; }
    IHotelCommand _Hotel { get;set; }
    public Commands(DatabaseContext context,IDataAccessor db, IMapper mapper)
    {
        _context = context;
        _db = db;
        _mapper = mapper;
    }
    public ICountryCommand Country => _Country ??= new CountryCommand(_context,_db,_mapper);
    public IHotelCommand Hotel => _Hotel ??= new HotelCommand(_context,_db,_mapper);
     
     public void Dispose() {
        _context.Dispose();
        GC.SuppressFinalize(this);
    }
    public async Task Save() {
        await _context.SaveChangesAsync();
    }
}
