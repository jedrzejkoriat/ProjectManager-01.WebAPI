namespace ProjectManager_01.Application.Contracts.Repositories;

public interface IRepository<T>
{
    Task<Guid> CreateAsync(T entity);
    Task<T> GetByIdAsync(Guid id);
    Task<List<T>> GetAllAsync();
    Task<bool> UpdateAsync(T entity);
    Task<bool> DeleteAsync(Guid id);
}
