using ProjectManager_01.Application.DTOs.Tags;

namespace ProjectManager_01.Application.Contracts.Services;

public interface ITagService
{
    Task CreateTagAsync(TagCreateDto tagCreateDto);
    Task UpdateTagAsync(TagUpdateDto tagUpdateDto);
    Task DeleteTagAsync(Guid tagId);
    Task<TagDto> GetTagByIdAsync(Guid tagId);
    Task<List<TagDto>> GetAllTagsAsync();
    Task<IEnumerable<TagDto>> GetTagsByProjectIdAsync(Guid projectId);
}
