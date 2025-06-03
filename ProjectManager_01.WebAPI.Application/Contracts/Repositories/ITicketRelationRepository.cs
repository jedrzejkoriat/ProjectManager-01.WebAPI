using System.Data;
using ProjectManager_01.Domain.Models;

namespace ProjectManager_01.Application.Contracts.Repositories;

public interface ITicketRelationRepository : IRepository<TicketRelation>
{
    Task<bool> DeleteByTicketIdAsync(Guid ticketId, IDbTransaction transaction);
}
