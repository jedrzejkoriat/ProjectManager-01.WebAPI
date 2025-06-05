using System.Data;
using ProjectManager_01.Application.DTOs.Tickets;

namespace ProjectManager_01.Application.Contracts.Services;

public interface ITicketService
{
    Task DeleteByProjectIdAsync(Guid projectId, IDbTransaction transaction);
    Task CreateTicketAsync(TicketCreateDto ticketCreateDto);
    Task<TicketDto> UpdateTicketAsync(TicketUpdateDto ticketUpdateDto);
    Task DeleteTicketAsync(Guid ticketId);
    Task<TicketDto> GetTicketByIdAsync(Guid ticketId);
    Task<IEnumerable<TicketDto>> GetAllTicketsAsync();
    Task ClearUserAssignmentAsync(Guid userId, IDbTransaction transaction);
    Task DeleteTicketByUserIdAsync(Guid userId, IDbTransaction transaction);
    Task SoftDeleteTicketAsync(Guid ticketId);
    Task DeleteTicketByPriorityIdAsync(Guid priorityId, IDbTransaction transaction);
    Task<TicketDto> GetTicketByKeyAndNumberAsync(string projectKey, int ticketNumber);
    Task<IEnumerable<TicketDto>> GetTicketsByProjectIdAsync(Guid projectId);
}
