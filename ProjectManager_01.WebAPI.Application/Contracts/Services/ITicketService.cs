using System.Data;
using ProjectManager_01.Application.DTOs.Tickets;

namespace ProjectManager_01.Application.Contracts.Services;

public interface ITicketService
{
    Task DeleteByProjectIdAsync(Guid projectId, IDbConnection connection, IDbTransaction transaction);
    Task CreateTicketAsync(TicketCreateDto ticketCreateDto);
    Task UpdateTicketAsync(TicketUpdateDto ticketUpdateDto);
    Task DeleteTicketAsync(Guid ticketId);
    Task<TicketDto> GetTicketByIdAsync(Guid ticketId);
    Task<List<TicketDto>> GetAllTicketsAsync();
    Task ClearUserReferencesAsync(Guid userId, IDbConnection connection, IDbTransaction transaction);
}
