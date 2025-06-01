namespace ProjectManager_01.Application.Contracts.Repositories.Base;
public interface IReadable<T>
{
    Task<T> GetByIdAsync(Guid id);
    Task<List<T>> GetAllAsync();
}
