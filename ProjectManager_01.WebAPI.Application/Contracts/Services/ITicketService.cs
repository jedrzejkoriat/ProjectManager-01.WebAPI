using ProjectManager_01.Application.DTOs.Tickets;

namespace ProjectManager_01.Application.Contracts.Services;

public interface ITicketService
{
    Task CreateTicketAsync(TicketCreateDto ticketCreateDto);
    Task UpdateTicketAsync(TicketUpdateDto ticketUpdateDto);
    Task DeleteTicketAsync(Guid ticketId);
    Task<TicketDto> GetTicketByIdAsync(Guid ticketId);
    Task<List<TicketDto>> GetAllTicketsAsync();
}
