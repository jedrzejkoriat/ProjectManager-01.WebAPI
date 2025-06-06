using System.Data;
using ProjectManager_01.Domain.Models;

namespace ProjectManager_01.Application.Contracts.Repositories;

public interface ITicketRepository : IRepository<Ticket>
{
    Task<Guid> CreateAsync(Ticket entity, IDbTransaction transaction);
    Task<bool> DeleteAsync(Guid id, IDbTransaction transaction);
    Task<IEnumerable<Ticket>> GetByProjectIdAsync(Guid projectId);
    Task<Ticket> GetByKeyAndNumberAsync(string key, int ticketNumber);
    Task<bool> SoftDeleteAsync(Guid id);
    Task<bool> DeleteByProjectIdAsync(Guid projectId, IDbTransaction transaction);
    Task<bool> ClearUserAssignmentAsync(Guid userId, IDbTransaction transaction);
    Task<bool> DeleteByUserIdAsync(Guid userId, IDbTransaction transaction);
    Task<bool> DeleteByPriorityIdAsync(Guid priorityId, IDbTransaction transaction);
    Task<IEnumerable<Ticket>> GetByPriorityIdAsync(Guid priorityId);
    Task<IEnumerable<Ticket>> GetByReporterIdAsync(Guid reporterId, IDbTransaction transaction);
}
