using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectManager_01.Application.Contracts.Repositories;
using ProjectManager_01.Domain.Models;

namespace ProjectManager_01.Infrastructure.Repositories;
internal class TagRepository : ITagRepository
{
    public Task<Guid> CreateAsync(Tag entity)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteAsync(Guid id)
    {
        throw new NotImplementedException();
    }

	public Task<List<Tag>> GetAllAsync()
	{
		throw new NotImplementedException();
	}

	public Task<Tag> GetByIdAsync(Guid id)
	{
		throw new NotImplementedException();
	}

	public Task<List<Tag>> GetByProjectIdAsync(Guid projectId)
    {
        throw new NotImplementedException();
    }

	public Task<bool> UpdateAsync(Tag entity)
	{
		throw new NotImplementedException();
	}
}
