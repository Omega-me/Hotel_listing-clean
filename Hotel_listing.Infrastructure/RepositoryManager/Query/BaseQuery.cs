﻿using System.Linq.Expressions;
using Hotel_listing.Application.Contracts.RepositoryManager.Options;
using Hotel_listing.Application.Contracts.RepositoryManager.Query;
using Hotel_listing.Infrastructure.DatabaseManager.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;
using Microsoft.VisualBasic.CompilerServices;
using Utils = Hotel_listing.Application.Utilities.Utils;

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
    public async Task<List<T>> GetAll(Options<T> options)
    {
        IQueryable<T> query = _db;
        
        //Add expressions
        if (options.Expression != null)
        {
            query = query.Where(options.Expression);
        }

        //Add filters
        if (options.Filter != null)
        {
            List<string> filters = Utils.QueryFilterTransformer(options.Filter);
            string filter = filters[0];
            string[] arr = filters.Skip(1).ToArray();
            query = query.Where(filter,arr);
        }
                
        //Include relations
        if (options.Includes != null)
        {
            string[] includeArray = options.Includes.Split(",").ToArray();
            foreach (var includeProperty in includeArray)
            {
                query = query.Include(includeProperty);
            }
        }

        //Add sorting
        //sort by fields seperated by "," and add _desc after the field for descending order
        if (options.Sort != null)
        {
            query = query.OrderBy(Utils.QuerySortTransformer(options.Sort));
        }

        //Add OrderBy
        if (options.OrderBy != null)
        {
            query = options.OrderBy(query);
        }
        
        //Add pagination
        if (options.Pagination != null)
        {
            var number = options.Pagination.PageNumber;
            var size = options.Pagination.PageSize;
            var skip = (number - 1) * size;
            query = query.Skip(skip).Take(size);
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

