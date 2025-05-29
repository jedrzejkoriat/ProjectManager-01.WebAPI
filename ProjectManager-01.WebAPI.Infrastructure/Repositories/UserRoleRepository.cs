using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectManager_01.Application.Contracts.Repositories;
using ProjectManager_01.Domain.Models;

namespace ProjectManager_01.Infrastructure.Repositories;
internal class UserRoleRepository : IUserRoleRepository
{
    public Task<Guid> CreateAsync(UserRole entity)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteAsync(Guid id1, Guid id2)
    {
        throw new NotImplementedException();
    }

    public Task<UserRole> GetByUserIdAsync(Guid userId)
    {
        throw new NotImplementedException();
    }
}
