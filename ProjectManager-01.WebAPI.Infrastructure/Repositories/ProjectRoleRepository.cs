using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectManager_01.Application.Contracts.Repositories;
using ProjectManager_01.Domain.Models;

namespace ProjectManager_01.Infrastructure.Repositories;
internal class ProjectRoleRepository : IProjectRoleRepository
{
    public Task<Guid> CreateAsync(ProjectRole entity)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<List<ProjectRole>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<ProjectRole> GetByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<bool> UpdateAsync(ProjectRole entity)
    {
        throw new NotImplementedException();
    }
}
