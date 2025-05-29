using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectManager_01.Application.Contracts.Repositories;
using ProjectManager_01.Domain.Models;

namespace ProjectManager_01.Infrastructure.Repositories;
internal class PermissionRepository : IPermissionRepository
{
    public Task<IEnumerable<Permission>> GetAllAsync()
    {
        throw new NotImplementedException();
    }
}
