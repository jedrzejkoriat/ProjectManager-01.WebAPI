using System.Data;
using ProjectManager_01.Domain.Models;

namespace ProjectManager_01.Application.Contracts.Repositories;

public interface IProjectRepository : IRepository<Project>
{
    Task<bool> DeleteByIdAsync(Guid id, IDbTransaction transaction);
    Task<bool> SoftDeleteByIdAsync(Guid id);
    Task<IEnumerable<Project>> GetAllByUserIdAsync(Guid userId);
}
