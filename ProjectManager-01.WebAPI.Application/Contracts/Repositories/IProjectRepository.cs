using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectManager_01.Domain.Models;

namespace ProjectManager_01.Application.Contracts.Repositories;
public interface IProjectRepository : IGenericRepository<Project>
{
    Task<Project> GetByIdAsync(Guid id);
    Task<List<Project>> GetAllAsync();
    Task<Guid> CreateAsync(Project project);
    Task<bool> UpdateAsync(Project project);
}
