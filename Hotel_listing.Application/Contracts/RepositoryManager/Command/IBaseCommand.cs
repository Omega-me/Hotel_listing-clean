namespace Hotel_listing.Application.Contracts.RepositoryManager.Command;

public interface IBaseCommand<T> where T:class
{
    Task Insert(T entity);
    Task InsertRange(IEnumerable<T> enitites);
    Task Delete(int id);
    void DeleteRange(IEnumerable<T> enitites);
    void Update(T entity);       
}