namespace ProjectManager_01.Application.Contracts.Repositories.Base;
public interface IUpdateable<T>
{
    Task<bool> UpdateAsync(T entity);
}
