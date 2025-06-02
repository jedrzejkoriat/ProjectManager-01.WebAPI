using ProjectManager_01.Application.DTOs.TicketTags;

namespace ProjectManager_01.Application.Contracts.Services;

public interface ITicketTagService
{
    Task CreateTicketTagAsync(TicketTagCreateDto ticketTagCreateDto);
    Task DeleteTicketTagAsync(Guid ticketId, Guid tagId);
    Task<TicketTagDto> GetTicketTagByIdAsync(Guid ticketId, Guid tagId);
    Task<List<TicketTagDto>> GetAllTicketTagsAsync();
}
