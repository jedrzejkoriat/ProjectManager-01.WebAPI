using System.Data;
using ProjectManager_01.Domain.Models;

namespace ProjectManager_01.Application.Contracts.Repositories;

public interface ICommentRepository : IRepository<Comment>
{
    Task<List<Comment>> GetByTicketIdAsync(Guid ticketId);
    Task<bool> DeleteByUserIdAsync(Guid userId, IDbTransaction transaction);
    Task<bool> DeleteByTicketIdAsync(Guid ticketId, IDbTransaction transaction);
}
