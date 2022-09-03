using System.Linq.Expressions;
using Hotel_listing.Application.Contracts.RepositoryManager.Query;
using Hotel_listing.Infrastructure.DatabaseManager.Context;
using Microsoft.EntityFrameworkCore;

namespace Hotel_listing.Infrastructure.RepositoryManager.Query;

public class BaseQuery<T> : IBaseQuery<T> where T : class
{
    private readonly DatabaseContext _context;

    private readonly DbSet<T> _db;

    public BaseQuery(DatabaseContext context)
    {
        _context = context;
        _db = _context.Set<T>();
    }

    public async Task<List<T>> GetAll(Expression<Func<T, bool>>? expression = null,
        Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, List<string>? includes = null)
    {
        IQueryable<T> query = _db;
        if (expression != null)
        {
            query = query.Where(expression);
        }

        if (includes != null)
        {
            foreach (var includeProperty in includes)
            {
                query = query.Include(includeProperty);
            }
        }

        if (orderBy != null)
        {
            query = orderBy(query);
        }

        return await query.AsNoTracking().ToListAsync();
    }

    public async Task<T> Get(Expression<Func<T, bool>> expression, List<string>? includes = null)
    {
        IQueryable<T> query = _db;
        if (includes != null)
        {
            foreach (var includeProperty in includes)
            {
                query = query.Include(includeProperty);
            }
        }

        return await query.AsNoTracking().FirstOrDefaultAsync(expression);
    }
}

