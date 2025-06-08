using System.Data;
using ProjectManager_01.Domain.Models;

namespace ProjectManager_01.Application.Contracts.Repositories;

public interface ITagRepository : IRepository<Tag>
{
    Task<IEnumerable<Tag>> GetAllByProjectIdAsync(Guid projectId);
    Task<IEnumerable<Tag>> GetAllByTicketIdAsync(Guid ticketId);
    Task<bool> DeleteAllByProjectIdAsync(Guid projectId, IDbTransaction transaction);
}
