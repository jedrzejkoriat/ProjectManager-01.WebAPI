using System.Data;
using ProjectManager_01.Domain.Models;

namespace ProjectManager_01.Application.Contracts.Repositories;

public interface ITicketRepository : IRepository<Ticket>
{
    Task<bool> CreateAsync(Ticket entity, IDbTransaction transaction);
    Task<bool> DeleteAsync(Guid id, IDbTransaction transaction);
    Task<IEnumerable<Ticket>> GetAllByProjectIdAsync(Guid projectId);
    Task<Ticket> GetByProjectKeyAndTicketNumberAsync(string key, int ticketNumber);
    Task<bool> SoftDeleteByIdAsync(Guid id);
    Task<bool> DeleteAllByProjectIdAsync(Guid projectId, IDbTransaction transaction);
    Task<bool> ClearUserAssignmentsAsync(Guid userId, IDbTransaction transaction);
    Task<bool> DeleteAllByUserIdAsync(Guid userId, IDbTransaction transaction);
    Task<bool> DeleteAllByPriorityIdAsync(Guid priorityId, IDbTransaction transaction);
    Task<IEnumerable<Ticket>> GetAllByPriorityIdAsync(Guid priorityId);
    Task<IEnumerable<Ticket>> GetAllByReporterIdAsync(Guid reporterId, IDbTransaction transaction);
}
