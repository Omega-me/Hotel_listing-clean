﻿using System.Linq.Expressions;
using Hotel_listing.Application.Contracts.RepositoryManager.Query;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;
using Hotel_listing.Application.Common.RepositoryOptions;
using Hotel_listing.Application.Common.Utilities;
using Hotel_listing.Persistence.Contexts;
using X.PagedList;

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
    public async Task<QueryReturn<T>> GetAll(Options<T> options)
    {
        IQueryable<T> query = _db;
        
        //Add expressions
        if (options.Expression != null)
        {
            query = query.Where(options.Expression);
        }
        
        //Add filters
        if (!string.IsNullOrWhiteSpace(options.Filter))
        {
            var filters = Utils.QueryFilterTransformer(options.Filter);
            string filter = filters[0];
            var arr = filters.Skip(1).ToArray();
            query = query.Where(filter,arr);
        }
                
        //Include relations
        if (!string.IsNullOrWhiteSpace(options.Includes))
        {
            string[] includeArray = options.Includes.Split(",").ToArray();
            foreach (var includeProperty in includeArray)
            {
                query = query.Include(includeProperty);
            }
        }

        //Add sorting
        //sort by fields seperated by "," and add _desc after the field for descending order
        if (!string.IsNullOrWhiteSpace(options.Sort))
        {
            query = query.OrderBy(Utils.QuerySortTransformer(options.Sort));
        }
        else
        {
            query = query.OrderBy("id");
        }

        //Add OrderBy
        if (options.OrderBy != null)
        {
            query = options.OrderBy(query);
        }

        return new QueryReturn<T>
        {
            Results = await query.AsNoTracking()
                .ToPagedListAsync(options.Pagination.PageNumber, options.Pagination.PageSize),
            ResultsCount = query.AsNoTracking().Count()
        };
    }

    public async Task<T> Get(Expression<Func<T, bool>> expression, string? includes = null)
    {
        IQueryable<T> query = _db;
        
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

