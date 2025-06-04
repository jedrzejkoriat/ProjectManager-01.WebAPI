using ProjectManager_01.Domain.Models;

namespace ProjectManager_01.Application.Contracts.Repositories;

public interface ITagRepository : IRepository<Tag>
{
    Task<IEnumerable<Tag>> GetByProjectIdAsync(Guid projectId);
    Task<IEnumerable<Tag>> GetByTicketIdAsync(Guid ticketId);
}
