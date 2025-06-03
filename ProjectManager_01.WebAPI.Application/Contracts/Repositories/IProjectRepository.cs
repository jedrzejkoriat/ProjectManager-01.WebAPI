using System.Data;
using ProjectManager_01.Domain.Models;

namespace ProjectManager_01.Application.Contracts.Repositories;

public interface IProjectRepository : IRepository<Project>
{
    Task<bool> DeleteAsync(Guid id, IDbConnection connection, IDbTransaction transaction);
    Task<bool> SoftDeleteAsync(Guid id);
}
