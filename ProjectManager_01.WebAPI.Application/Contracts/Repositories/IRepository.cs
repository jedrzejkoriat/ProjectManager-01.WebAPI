namespace ProjectManager_01.Application.Contracts.Repositories;

public interface IRepository<T>
{
    Task<bool> CreateAsync(T entity);
    Task<T> GetByIdAsync(Guid id);
    Task<IEnumerable<T>> GetAllAsync();
    Task<bool> UpdateAsync(T entity);
    Task<bool> DeleteByIdAsync(Guid id);
}
