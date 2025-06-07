using AutoMapper;
using ProjectManager_01.Application.Contracts.Auth;
using ProjectManager_01.Application.Contracts.Repositories;
using ProjectManager_01.Application.Contracts.Services;
using ProjectManager_01.Application.DTOs.Tags;
using ProjectManager_01.Domain.Models;

namespace ProjectManager_01.Application.Services;

public class TagService : ITagService
{
    private readonly ITagRepository _tagRepository;
    private readonly IProjectAccessValidator _projectValidatorHelper;
    private readonly IMapper _mapper;

    public TagService(
        ITagRepository tagRepository,
        IProjectAccessValidator projectValidatorHelper,
        IMapper mapper)
    {
        _tagRepository = tagRepository;
        _projectValidatorHelper = projectValidatorHelper;
        _mapper = mapper;
    }

    public async Task CreateTagAsync(TagCreateDto tagCreateDto, Guid projectId)
    {
        _projectValidatorHelper.ValidateProjectIds(tagCreateDto.ProjectId, projectId);

        var tag = _mapper.Map<Tag>(tagCreateDto);
        await _tagRepository.CreateAsync(tag);
    }

    public async Task UpdateTagAsync(TagUpdateDto tagUpdateDto, Guid projectId)
    {
        _projectValidatorHelper.ValidateProjectIds(tagUpdateDto.ProjectId, projectId);

        var tag = _mapper.Map<Tag>(tagUpdateDto);
        await _tagRepository.UpdateAsync(tag);
    }

    public async Task DeleteTagAsync(Guid tagId, Guid projectId)
    {
        _projectValidatorHelper.ValidateProjectIds((await _tagRepository.GetByIdAsync(tagId)).ProjectId, projectId);

        await _tagRepository.DeleteAsync(tagId);
    }

    public async Task<TagDto> GetTagByIdAsync(Guid tagId, Guid projectId)
    {
        var tag = await _tagRepository.GetByIdAsync(tagId);
        _projectValidatorHelper.ValidateProjectIds(tag.ProjectId, projectId);

        return _mapper.Map<TagDto>(tag);
    }

    public async Task<IEnumerable<TagDto>> GetAllTagsAsync()
    {
        var tags = await _tagRepository.GetAllAsync();

        return _mapper.Map<IEnumerable<TagDto>>(tags);
    }

    public async Task<IEnumerable<TagDto>> GetTagsByProjectIdAsync(Guid projectId)
    {
        var tags = await _tagRepository.GetByProjectIdAsync(projectId);

        return _mapper.Map<IEnumerable<TagDto>>(tags);
    }

    public async Task<IEnumerable<TagDto>> GetTagsByTicketIdAsync(Guid ticketId)
    {
        var tags = await _tagRepository.GetByTicketIdAsync(ticketId);

        return _mapper.Map<IEnumerable<TagDto>>(tags);
    }
}
