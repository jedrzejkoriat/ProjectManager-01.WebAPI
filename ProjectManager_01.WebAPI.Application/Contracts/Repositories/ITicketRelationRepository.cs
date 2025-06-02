using ProjectManager_01.Domain.Models;

namespace ProjectManager_01.Application.Contracts.Repositories;

public interface ITicketRelationRepository : IGenericRepository<TicketRelation>
{
    Task<List<TicketRelation>> GetBySourceIdAsync(Guid sourceId);
    Task<List<TicketRelation>> GetByTargetIdAsync(Guid targetId);
}
