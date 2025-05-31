using ProjectManager_01.Application.Contracts.Repositories;
using ProjectManager_01.Domain.Models;

namespace ProjectManager_01.Infrastructure.Repositories;
internal class TicketTagRepository : ITicketTagRepository
{
    public Task<Guid> CreateAsync(TicketTag entity)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteAsync(Guid id1, Guid id2)
    {
        throw new NotImplementedException();
    }

	public Task<bool> DeleteAsync(Guid id)
	{
		throw new NotImplementedException();
	}

	public Task<List<TicketTag>> GetAllAsync()
	{
		throw new NotImplementedException();
	}

	public Task<TicketTag> GetByIdAsync(Guid id)
	{
		throw new NotImplementedException();
	}

	public Task<List<TicketTag>> GetByTagIdAsync(Guid tagId)
    {
        throw new NotImplementedException();
    }

    public Task<List<TicketTag>> GetByTicketIdAsync(Guid ticketId)
    {
        throw new NotImplementedException();
    }

	public Task<bool> UpdateAsync(TicketTag entity)
	{
		throw new NotImplementedException();
	}
}
