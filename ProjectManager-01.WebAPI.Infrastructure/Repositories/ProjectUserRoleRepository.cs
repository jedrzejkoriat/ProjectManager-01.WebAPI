using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectManager_01.Application.Contracts.Repositories;
using ProjectManager_01.Domain.Models;

namespace ProjectManager_01.Infrastructure.Repositories;
internal class ProjectUserRoleRepository : IProjectUserRoleRepository
{
    public Task<Guid> CreateAsync(ProjectUserRole entity)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<List<ProjectUserRole>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<ProjectUserRole> GetByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<bool> UpdateAsync(ProjectUserRole entity)
    {
        throw new NotImplementedException();
    }
}
