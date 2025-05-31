using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectManager_01.Application.Contracts.Repositories;
using ProjectManager_01.Domain.Models;

namespace ProjectManager_01.Infrastructure.Repositories;
internal class RoleRepository : IRoleRepository
{
	public Task<Guid> CreateAsync(Role entity)
	{
		throw new NotImplementedException();
	}

	public Task<bool> DeleteAsync(Guid id)
	{
		throw new NotImplementedException();
	}

	public Task<List<Role>> GetAllAsync()
	{
		throw new NotImplementedException();
	}

	public Task<Role> GetByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }

	public Task<bool> UpdateAsync(Role entity)
	{
		throw new NotImplementedException();
	}
}
