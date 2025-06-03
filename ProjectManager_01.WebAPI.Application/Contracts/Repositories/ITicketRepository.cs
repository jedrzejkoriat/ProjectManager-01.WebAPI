using System.Data;
using ProjectManager_01.Domain.Enums;
using ProjectManager_01.Domain.Models;

namespace ProjectManager_01.Application.Contracts.Repositories;

public interface ITicketRepository : IRepository<Ticket>
{
    Task<bool> DeleteAsync(Guid id, IDbConnection connection, IDbTransaction transaction);
    Task<List<Ticket>> GetByProjectIdAsync(Guid projectId);
    Task<Ticket> GetByKeyAndNumberAsync(string key, int ticketNumber);
    Task<List<Ticket>> GetByPriorityAsync(Guid priorityId);
    Task<List<Ticket>> GetByReporterIdAsync(Guid reporterId, IDbConnection connection, IDbTransaction transaction);
    Task<List<Ticket>> GetByAssigneeIdAsync(Guid? assigneeId);
    Task<List<Ticket>> GetByStatusAsync(Status status);
    Task<List<Ticket>> GetByTicketTypeAsync(TicketType ticketType);
    Task<List<Ticket>> GetByResolutionAsync(Resolution resolution);
    Task<bool> SoftDeleteAsync(Guid id);
    Task<bool> DeleteByProjectIdAsync(Guid projectId, IDbConnection connection, IDbTransaction transaction);
    Task<bool> ClearUserAssignmentAsync(Guid userId, IDbConnection connection, IDbTransaction transaction);
    Task<bool> DeleteByUserIdAsync(Guid userId, IDbConnection connection, IDbTransaction transaction);
    Task<bool> DeleteByPriorityIdAsync(Guid priorityId, IDbConnection connection, IDbTransaction transaction);
}
