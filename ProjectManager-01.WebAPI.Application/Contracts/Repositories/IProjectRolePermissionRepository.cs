using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectManager_01.Application.Contracts.Repositories.Base;
using ProjectManager_01.Domain.Models;

namespace ProjectManager_01.Application.Contracts.Repositories;
public interface IProjectRolePermissionRepository
{
    Task<bool> CreateAsync(ProjectRolePermission projectRolePermission);
    Task<bool> DeleteAsync(Guid projectRoleId, Guid permissionId);
    Task<bool> DeleteByProjectRoleIdAsync(Guid projectRoleId);
    Task<bool> DeleteByPermissionIdAsync(Guid permissionId);
}
