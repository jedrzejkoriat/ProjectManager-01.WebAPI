using System.Data;
using ProjectManager_01.Domain.Models;

namespace ProjectManager_01.Application.Contracts.Repositories;

public interface ICommentRepository : IRepository<Comment>
{
    Task<List<Comment>> GetByTicketIdAsync(Guid ticketId);
    Task<List<Comment>> GetByUserAndProjectIdAsync(Guid userId, Guid projectId);
    Task<bool> DeleteByUserIdAsync(Guid userId, IDbConnection connection, IDbTransaction transaction);
    Task<bool> DeleteByTicketIdAsync(Guid ticketId, IDbConnection connection, IDbTransaction transaction);
}
