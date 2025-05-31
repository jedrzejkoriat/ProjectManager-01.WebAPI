using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectManager_01.Application.Contracts.Repositories;
using ProjectManager_01.Domain.Models;

namespace ProjectManager_01.Infrastructure.Repositories;
internal class TicketRelationRepository : ITicketRelationRepository
{
    public Task<Guid> CreateAsync(TicketRelation entity)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteAsync(Guid id)
    {
        throw new NotImplementedException();
    }

	public Task<List<TicketRelation>> GetAllAsync()
	{
		throw new NotImplementedException();
	}

	public Task<TicketRelation> GetByIdAsync(Guid id)
	{
		throw new NotImplementedException();
	}

	public Task<List<TicketRelation>> GetBySourceIdAsync(Guid sourceId)
    {
        throw new NotImplementedException();
    }

    public Task<List<TicketRelation>> GetByTargetIdAsync(Guid targetId)
    {
        throw new NotImplementedException();
    }

	public Task<bool> UpdateAsync(TicketRelation entity)
	{
		throw new NotImplementedException();
	}
}
