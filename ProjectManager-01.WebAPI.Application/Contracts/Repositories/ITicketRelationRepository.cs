using ProjectManager_01.Application.Contracts.Repositories.Base;
using ProjectManager_01.Domain.Models;
namespace ProjectManager_01.Application.Contracts.Repositories;
public interface ITicketRelationRepository :
    ICreateable<TicketRelation>, IReadable<TicketRelation>, IUpdateable<TicketRelation>, IDeleteable
{
    Task<List<TicketRelation>> GetBySourceIdAsync(Guid sourceId);
    Task<List<TicketRelation>> GetByTargetIdAsync(Guid targetId);
}
