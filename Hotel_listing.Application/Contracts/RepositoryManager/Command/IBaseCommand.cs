using Microsoft.AspNetCore.JsonPatch;

namespace Hotel_listing.Application.Contracts.RepositoryManager.Command;

public interface IBaseCommand<T> where T:class
{
    Task Insert(T entity);
    void Update(T entity);      
    void UpdatePartial(T entity,JsonPatchDocument data);
    Task Delete(int id);
}