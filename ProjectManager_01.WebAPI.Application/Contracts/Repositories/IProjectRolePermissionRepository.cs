using System.Data;
using System.Transactions;
using ProjectManager_01.Domain.Models;

namespace ProjectManager_01.Application.Contracts.Repositories;

public interface IProjectRolePermissionRepository
{
    Task<ProjectRolePermission> GetByIdAsync(Guid projectRoleId, Guid permissionId);
    Task<List<ProjectRolePermission>> GetAllAsync();
    Task<bool> CreateAsync(ProjectRolePermission projectRolePermission);
    Task<bool> DeleteAsync(Guid projectRoleId, Guid permissionId);
    Task<bool> DeleteByProjectRoleIdAsync(Guid projectRoleId, IDbTransaction transaction);
    Task<bool> DeleteByPermissionIdAsync(Guid permissionId, IDbTransaction transaction);
    Task<bool> CreateAsync(ProjectRolePermission projectRolePermission, IDbTransaction transaction);
}
