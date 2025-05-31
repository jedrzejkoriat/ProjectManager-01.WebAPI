using ProjectManager_01.Application.Contracts.Repositories.Base;
using ProjectManager_01.Domain.Models;

namespace ProjectManager_01.Application.Contracts.Repositories;
public interface IUserRepository : 
    ICreateable<User>, IReadable<User>, IUpdateable<User>, IDeleteable, ISoftDeletable
{
    Task<List<User>> GetByProjectIdAsync(Guid projectId);
}
