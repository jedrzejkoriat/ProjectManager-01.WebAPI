using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectManager_01.Application.Contracts.Repositories.Base;
using ProjectManager_01.Domain.Models;

namespace ProjectManager_01.Application.Contracts.Repositories;
public interface IRoleRepository : ICreateable<Role>, IReadable<Role>, IUpdateable<Role>, IDeleteable
{
    Task<Role> GetByIdAsync(Guid id);
}
