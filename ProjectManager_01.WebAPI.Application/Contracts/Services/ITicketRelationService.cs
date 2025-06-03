using System.Data;
using ProjectManager_01.Application.DTOs.TicketRelations;

namespace ProjectManager_01.Application.Contracts.Services;

public interface ITicketRelationService
{
    Task CreateTicketRelationAsync(TicketRelationCreateDto ticketRelationCreateDto);
    Task UpdateTicketRelationAsync(TicketRelationUpdateDto ticketRelationUpdateDto);
    Task DeleteTicketRelationAsync(Guid ticketRelationId);
    Task<TicketRelationDto> GetTicketRelationByIdAsync(Guid ticketRelationId);
    Task<List<TicketRelationDto>> GetAllTicketRelationsAsync();
    Task DeleteTicketRelationByTicketIdAsync(Guid ticketId, IDbConnection connection, IDbTransaction transaction);
}
