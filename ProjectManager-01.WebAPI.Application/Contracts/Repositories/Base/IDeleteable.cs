namespace ProjectManager_01.Application.Contracts.Repositories.Base;
public interface IDeleteable
{
    Task<bool> DeleteAsync(Guid id);
}
