using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectManager_01.Application.Contracts.Repositories;
using ProjectManager_01.Domain.Models;

namespace ProjectManager_01.Infrastructure.Repositories;
internal class ProjectRepository : IProjectRepository
{
    public Task<Guid> CreateAsync(Project entity)
    {
        throw new NotImplementedException();
    }

    public Task<Project> GetByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<List<Project>> GetByUserIdAsync(Guid userId)
    {
        throw new NotImplementedException();
    }

    public Task<bool> SoftDeleteAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<bool> UpdateAsync(Project entity)
    {
        throw new NotImplementedException();
    }
}
