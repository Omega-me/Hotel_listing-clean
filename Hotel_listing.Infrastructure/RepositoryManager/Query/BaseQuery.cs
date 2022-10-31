using System.Linq.Expressions;
using Hotel_listing.Application.Contracts.RepositoryManager.Query;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;
using AutoMapper;
using Hotel_listing.Application.Common.Features;
using Hotel_listing.Application.Common.Utilities;
using Hotel_listing.Application.Contracts.RepositoryManager.DataAccessor;
using Hotel_listing.Persistence.Contexts;
using X.PagedList;

namespace Hotel_listing.Infrastructure.RepositoryManager.Query;
public class BaseQuery<T> : IBaseQuery<T> where T : class
{
    protected readonly DatabaseContext Context;
    protected readonly IDataAccessor Db;
    protected readonly IMapper Mapper;
    protected readonly DbSet<T>? DataSet;

    public BaseQuery(DatabaseContext context,IDataAccessor db,IMapper mapper)
    {
        Context = context;
        Db = db;
        Mapper = mapper;
        DataSet = Context.Set<T>();
    }
    
    public async Task<QueryReturn<T>> GetAll(Features<T> features)
    {
        IQueryable<T>? query = DataSet;
        
        //Add expressions
        if (features.Expression != null)
        {
            query = query.Where(features.Expression);
        }
        
        //Add filters
        if (features.Filter!=null)
        {
            var filters = Utils.QueryFilterTransformer(features.Filter);
            string filter = filters[0];
            var values = filters.Skip(1).ToArray();
            query = query.Where(filter,values);
        }
                
        //Include relations
        if (features.Includes!=null)
        {
            string[] includeArray = features.Includes.Split(",").ToArray();
            foreach (var includeProperty in includeArray)
            {
                query = query.Include(includeProperty);
            }
        }
        
        //Add OrderBy
        if (features.OrderBy != null)
        {
            query = features.OrderBy(query);
        }

        //Add sorting
        if (features.Sort!=null)
        {
            query = query.OrderBy(Utils.QuerySortTransformer(features.Sort));
        }

        return new QueryReturn<T>
        {
            Results = await query.AsNoTracking()
                .ToPagedListAsync(features.Pagination.PageNumber, features.Pagination.PageSize),
            ResultsCount = query.AsNoTracking().Count()
        };
    }

    public async Task<T> Get(Expression<Func<T, bool>> expression, string? includes = null)
    {
        IQueryable<T>? query = DataSet;
        
        //Include relations
        if (includes != null)
        {
            string[] includeArray = includes.Split(",").ToArray();
            foreach (var includeProperty in includeArray)
            {
                query = query.Include(includeProperty);
            }
        }

        return await query.AsNoTracking().FirstOrDefaultAsync(expression);
    }
}

