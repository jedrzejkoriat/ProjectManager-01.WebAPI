using ProjectManager_01.Domain.Models;

namespace ProjectManager_01.Application.Contracts.Repositories;

public interface IProjectRepository : IGenericRepository<Project>
{
    Task<bool> SoftDeleteAsync(Guid id);
}
