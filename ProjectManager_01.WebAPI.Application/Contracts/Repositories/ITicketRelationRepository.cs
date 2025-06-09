using System.Data;
using ProjectManager_01.Domain.Models;

namespace ProjectManager_01.Application.Contracts.Repositories;

public interface ITicketRelationRepository : IRepository<TicketRelation>
{
    Task<bool> DeleteAllByTicketIdAsync(Guid ticketId, IDbTransaction transaction);
    Task<IEnumerable<TicketRelation>> GetAllBySourceIdAsync(Guid ticketId);
    Task<IEnumerable<TicketRelation>> GetAllByTargetIdAsync(Guid ticketId);
}
