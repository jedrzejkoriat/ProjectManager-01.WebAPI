using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectManager_01.Application.Contracts.Repositories;
using ProjectManager_01.Domain.Models;

namespace ProjectManager_01.Infrastructure.Repositories;
internal class ProjectRolePermissionRepository : IProjectRolePermissionRepository
{
    public Task<Guid> CreateAsync(ProjectRolePermission entity)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<List<ProjectRolePermission>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<ProjectRolePermission> GetByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<bool> UpdateAsync(ProjectRolePermission entity)
    {
        throw new NotImplementedException();
    }
}
