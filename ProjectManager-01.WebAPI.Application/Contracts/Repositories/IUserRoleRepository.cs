using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectManager_01.Application.Contracts.Repositories.Base;
using ProjectManager_01.Domain.Models;

namespace ProjectManager_01.Application.Contracts.Repositories;
public interface IUserRoleRepository : ICreateable<UserRole>, IReadable<UserRole>, IUpdateable<UserRole>, IDeleteable
{
    Task<UserRole> GetByUserIdAsync(Guid userId);
}
