using AutoMapper;
using ProjectManager_01.Application.Contracts.Repositories;
using ProjectManager_01.Application.Contracts.Services;
using ProjectManager_01.Application.DTOs.Tags;
using ProjectManager_01.Domain.Models;

namespace ProjectManager_01.Application.Services;

public class TagService : ITagService
{
    private readonly ITagRepository tagRepository;
    private readonly IMapper mapper;

    public TagService(ITagRepository tagRepository, IMapper mapper)
    {
        this.tagRepository = tagRepository;
        this.mapper = mapper;
    }

    public async Task CreateTagAsync(TagCreateDto tagCreateDto)
    {
        Tag tag = mapper.Map<Tag>(tagCreateDto);
        await tagRepository.CreateAsync(tag);
    }

    public async Task UpdateTagAsync(TagUpdateDto tagUpdateDto)
    {
        Tag tag = mapper.Map<Tag>(tagUpdateDto);
        await tagRepository.UpdateAsync(tag);
    }

    public async Task DeleteTagAsync(Guid tagId)
    {
        await tagRepository.DeleteAsync(tagId);
    }

    public async Task<TagDto> GetTagByIdAsync(Guid tagId)
    {
        Tag tag = await tagRepository.GetByIdAsync(tagId);

        return mapper.Map<TagDto>(tag);
    }

    public async Task<List<TagDto>> GetAllTagsAsync()
    {
        List<Tag> tags = await tagRepository.GetAllAsync();

        return mapper.Map<List<TagDto>>(tags);
    }

    public async Task<IEnumerable<TagDto>> GetTagsByProjectIdAsync(Guid projectId)
    {
        var tags = await tagRepository.GetByProjectIdAsync(projectId);

        return mapper.Map<IEnumerable<TagDto>>(tags);
    }

    public async Task<IEnumerable<TagDto>> GetTagsByTicketIdAsync(Guid ticketId)
    {
        var tags = tagRepository.GetByTicketIdAsync(ticketId);

        return mapper.Map<IEnumerable<TagDto>>(tags);
    }
}
