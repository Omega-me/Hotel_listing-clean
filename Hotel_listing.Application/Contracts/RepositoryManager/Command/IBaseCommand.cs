namespace Hotel_listing.Application.Contracts.RepositoryManager.Command;

public interface IBaseCommand<T> where T:class
{
    Task Insert(T entity);
    void Update(T entity);       
    Task Delete(int id);
    void DeleteRange(IEnumerable<T> enitites);
}