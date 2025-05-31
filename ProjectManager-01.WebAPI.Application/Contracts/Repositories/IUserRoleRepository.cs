using ProjectManager_01.Application.Contracts.Repositories.Base;
using ProjectManager_01.Domain.Models;

namespace ProjectManager_01.Application.Contracts.Repositories;
public interface IUserRoleRepository :
    ICreateable<UserRole>, IReadable<UserRole>, IDeleteable
{
    Task<List<UserRole>> GetByRoleIdAsync(Guid roleId);
}
