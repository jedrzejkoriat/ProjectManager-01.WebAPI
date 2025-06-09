using System.Data;
using ProjectManager_01.Domain.Models;

namespace ProjectManager_01.Application.Contracts.Repositories;

public interface ICommentRepository : IRepository<Comment>
{
    Task<IEnumerable<Comment>> GetAllByTicketIdAsync(Guid ticketId);
    Task<bool> DeleteAllByUserIdAsync(Guid userId, IDbTransaction transaction);
    Task<bool> DeleteAllByTicketIdAsync(Guid ticketId, IDbTransaction transaction);
}
