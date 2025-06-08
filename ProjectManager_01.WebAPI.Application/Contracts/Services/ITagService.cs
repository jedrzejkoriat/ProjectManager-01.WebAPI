using System.Data;
using ProjectManager_01.Application.DTOs.Tags;

namespace ProjectManager_01.Application.Contracts.Services;

public interface ITagService
{
    Task CreateTagAsync(TagCreateDto tagCreateDto, Guid projectId);
    Task UpdateTagAsync(TagUpdateDto tagUpdateDto, Guid projectId);
    Task DeleteTagAsync(Guid tagId, Guid projectId);
    Task<TagDto> GetTagByIdAsync(Guid tagId, Guid projectId);
    Task<IEnumerable<TagDto>> GetAllTagsAsync();
    Task<IEnumerable<TagDto>> GetTagsByProjectIdAsync(Guid projectId);
    Task<IEnumerable<TagDto>> GetTagsByTicketIdAsync(Guid ticketId);
    Task DeleteByProjectIdAsync(Guid projectId, IDbTransaction transaction);
}
