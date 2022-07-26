using Hotel_listing.Application.Contracts.RepositoryManager.Command;
using Hotel_listing.Infrastructure.DatabaseManager.Context;
using Microsoft.EntityFrameworkCore;

namespace Hotel_listing.Infrastructure.RepositoryManager.Command;

public class BaseCommand<T>:IBaseCommand<T> where T:class
{
    private readonly DatabaseContext _context;
    private readonly DbSet<T> _db;

    public BaseCommand(DatabaseContext context)
    {
        _context = context;
        _db = _context.Set<T>();   
    }
    
    public async Task Insert(T entity) {
        await _db.AddAsync(entity);
    }

    public async Task InsertRange(IEnumerable<T> enitites) {
        await _db.AddRangeAsync(enitites);
    }

    public async Task Delete(int id) {
        var entity =await _db.FindAsync(id);
        _db.Remove(entity);
    }

    public void DeleteRange(IEnumerable<T> enitites) {
        _db.RemoveRange(enitites);
    }

    public void Update(T entity) {
        _db.Attach(entity);
        _context.Entry(entity).State = EntityState.Modified;
    }
}