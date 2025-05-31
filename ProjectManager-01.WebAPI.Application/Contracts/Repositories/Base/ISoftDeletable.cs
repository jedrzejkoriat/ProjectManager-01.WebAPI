namespace ProjectManager_01.Application.Contracts.Repositories.Base;
public interface ISoftDeletable
{
    Task<bool> SoftDeleteAsync(Guid id);
}
