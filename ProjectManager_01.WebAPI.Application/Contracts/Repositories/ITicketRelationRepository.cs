using System.Data;
using ProjectManager_01.Domain.Models;

namespace ProjectManager_01.Application.Contracts.Repositories;

public interface ITicketRelationRepository : IRepository<TicketRelation>
{
    Task<List<TicketRelation>> GetBySourceIdAsync(Guid sourceId);
    Task<List<TicketRelation>> GetByTargetIdAsync(Guid targetId);
    Task<bool> DeleteByTicketIdAsync(Guid ticketId, IDbConnection connection, IDbTransaction transaction);
}
