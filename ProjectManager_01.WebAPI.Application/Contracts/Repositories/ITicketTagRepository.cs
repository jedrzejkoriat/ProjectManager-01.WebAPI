using System.Data;
using ProjectManager_01.Domain.Models;

namespace ProjectManager_01.Application.Contracts.Repositories;

public interface ITicketTagRepository
{
    Task<List<TicketTag>> GetAllAsync();
    Task<TicketTag> GetByIdAsync(Guid ticketId, Guid tagId);
    Task<bool> DeleteAsync(Guid ticketId, Guid tagId);
    Task<bool> CreateAsync(TicketTag ticketTag);
    Task<bool> CreateAsync(TicketTag ticketTag, IDbTransaction dbTransaction);
}
