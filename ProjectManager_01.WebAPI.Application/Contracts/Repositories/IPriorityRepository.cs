using System.Data;
using ProjectManager_01.Domain.Models;

namespace ProjectManager_01.Application.Contracts.Repositories;

public interface IPriorityRepository : IRepository<Priority>
{
    Task<bool> DeleteByIdAsync(Guid id, IDbTransaction transaction);
}
