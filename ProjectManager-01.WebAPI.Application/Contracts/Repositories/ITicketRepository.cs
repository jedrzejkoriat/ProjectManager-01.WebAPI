using ProjectManager_01.Application.Contracts.Repositories.Base;
using ProjectManager_01.Domain.Enums;
using ProjectManager_01.Domain.Models;

namespace ProjectManager_01.Application.Contracts.Repositories;
public interface ITicketRepository : 
    ICreateable<Ticket>, IReadable<Ticket>, IUpdateable<Ticket>, IDeleteable, ISoftDeletable
{
    Task<List<Ticket>> GetByProjectIdAsync(Guid projectId);
    Task<Ticket> GetByKeyAndNumberAsync(string key, int ticketNumber);
    Task<List<Ticket>> GetByPriorityAsync(Guid priorityId);
    Task<List<Ticket>> GetByReporterIdAsync(Guid reporterId);
    Task<List<Ticket>> GetByAssigneeIdAsync(Guid? assigneeId);
    Task<List<Ticket>> GetByStatusAsync(Status status);
    Task<List<Ticket>> GetByTicketTypeAsync(TicketType ticketType);
    Task<List<Ticket>> GetByResolutionAsync(Resolution resolution);
}
