using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectManager_01.Domain.Models;

namespace ProjectManager_01.Application.Contracts.Repositories;
public interface IPermissionRepository
{
    Task<Permission> GetByIdAsync(Guid id);
    Task<List<Permission>> GetAllAsync();
}
