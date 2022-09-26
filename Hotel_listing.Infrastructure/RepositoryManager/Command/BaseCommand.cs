using AutoMapper;
using Hotel_listing.Application.Contracts.RepositoryManager.Command;
using Hotel_listing.Application.Contracts.RepositoryManager.DataAccessor;
using Hotel_listing.Persistence.Contexts;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;

namespace Hotel_listing.Infrastructure.RepositoryManager.Command;
public class BaseCommand<T>: IBaseCommand<T> where T:class
{
    protected readonly DatabaseContext Context;
    protected readonly IDataAccessor Db;
    protected readonly IMapper Mapper;
    protected readonly DbSet<T> DataSet;

    public BaseCommand(DatabaseContext context,IDataAccessor db,IMapper mapper)
    {
        Context = context;
        Db = db;
        Mapper = mapper;
        DataSet = Context.Set<T>();   
    }
    
    public async Task Insert(T entity) {
        await DataSet.AddAsync(entity);
    }

    public void Update(T entity) {
        DataSet.Attach(entity);
        Context.Entry(entity).State = EntityState.Modified;
    }

    public void UpdatePartial(T entity,JsonPatchDocument data) {
        data.ApplyTo(entity);
        Context.Entry(entity).State = EntityState.Modified;
    }
    public async Task Delete(int id)
    {
        var entity =await DataSet.FindAsync(id);
        if (entity != null) DataSet.Remove(entity);
    }
}