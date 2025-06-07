using System.Data;
using ProjectManager_01.Application.DTOs.TicketTags;

namespace ProjectManager_01.Application.Contracts.Services;

public interface ITicketTagService
{
    Task CreateTicketTagAsync(TicketTagCreateDto ticketTagCreateDto, Guid projectId);
    Task CreateTicketTagAsync(TicketTagCreateDto ticketTagCreateDto, IDbTransaction dbTransaction);
    Task DeleteTicketTagAsync(Guid ticketId, Guid tagId, Guid projectId);
    Task<TicketTagDto> GetTicketTagByIdAsync(Guid ticketId, Guid tagId, Guid projectId);
    Task<IEnumerable<TicketTagDto>> GetAllTicketTagsAsync();
}
