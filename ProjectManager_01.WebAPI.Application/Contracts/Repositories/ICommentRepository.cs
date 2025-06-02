using ProjectManager_01.Domain.Models;

namespace ProjectManager_01.Application.Contracts.Repositories;

public interface ICommentRepository : IGenericRepository<Comment>
{
    Task<List<Comment>> GetByTicketIdAsync(Guid ticketId);
    Task<List<Comment>> GetByUserAndProjectIdAsync(Guid userId, Guid projectId);
}
