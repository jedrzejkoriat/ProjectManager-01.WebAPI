namespace ProjectManager_01.Application.Contracts.Repositories.Base;
public interface ICreateable<T>
{
    Task<Guid> CreateAsync(T entity);
}
