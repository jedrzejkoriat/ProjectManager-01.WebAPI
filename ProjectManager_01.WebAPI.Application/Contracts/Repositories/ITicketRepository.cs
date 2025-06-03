using System.Data;
using ProjectManager_01.Domain.Enums;
using ProjectManager_01.Domain.Models;

namespace ProjectManager_01.Application.Contracts.Repositories;

public interface ITicketRepository : IGenericRepository<Ticket>
{
    Task<List<Ticket>> GetByProjectIdAsync(Guid projectId);
    Task<Ticket> GetByKeyAndNumberAsync(string key, int ticketNumber);
    Task<List<Ticket>> GetByPriorityAsync(Guid priorityId);
    Task<List<Ticket>> GetByReporterIdAsync(Guid reporterId);
    Task<List<Ticket>> GetByAssigneeIdAsync(Guid? assigneeId);
    Task<List<Ticket>> GetByStatusAsync(Status status);
    Task<List<Ticket>> GetByTicketTypeAsync(TicketType ticketType);
    Task<List<Ticket>> GetByResolutionAsync(Resolution resolution);
    Task<bool> SoftDeleteAsync(Guid id);
    Task<bool> DeleteByProjectIdAsync(Guid projectId, IDbConnection connection, IDbTransaction transaction);
    Task<bool> ClearUserReferencesAsync(Guid userId, IDbConnection connection, IDbTransaction transaction);
}
