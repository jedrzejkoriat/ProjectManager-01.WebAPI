using System.Data;
using AutoMapper;
using Microsoft.Extensions.Logging;
using ProjectManager_01.Application.Contracts.Auth;
using ProjectManager_01.Application.Contracts.Repositories;
using ProjectManager_01.Application.Contracts.Services;
using ProjectManager_01.Application.DTOs.Tags;
using ProjectManager_01.Application.Exceptions;
using ProjectManager_01.Domain.Models;

namespace ProjectManager_01.Application.Services;

public class TagService : ITagService
{
    private readonly ITagRepository _tagRepository;
    private readonly IProjectAccessValidator _projectAccessValidator;
    private readonly IMapper _mapper;
    private readonly ILogger<TagService> _logger;

    public TagService(
        ITagRepository tagRepository,
        IProjectAccessValidator projectAccessValidator,
        IMapper mapper,
        ILogger<TagService> logger)
    {
        _tagRepository = tagRepository;
        _projectAccessValidator = projectAccessValidator;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task CreateTagAsync(TagCreateDto tagCreateDto, Guid projectId)
    {
        _logger.LogInformation("Creating Tag called. ProjectId: {ProjectId}, TagName: {TagName}", projectId, tagCreateDto.Name);

        // Validate if project access is allowed
        _projectAccessValidator.ValidateProjectIds(tagCreateDto.ProjectId, projectId);

        var tag = _mapper.Map<Tag>(tagCreateDto);
        tag.Id = Guid.NewGuid();

        if (!await _tagRepository.CreateAsync(tag))
        {
            _logger.LogError("Creating Tag failed. ProjectId: {ProjectId}, TagName: {TagName}", projectId, tag.Name);
            throw new OperationFailedException("Creating Tag failed.");
        }

        _logger.LogInformation("Creating Tag successful. TagId: {TagId}", tag.Id);
    }

    public async Task UpdateTagAsync(TagUpdateDto tagUpdateDto, Guid projectId)
    {
        _logger.LogInformation("Updating Tag called. TagId: {TagId}", tagUpdateDto.Id);

        // Validate if project access is allowed
        _projectAccessValidator.ValidateProjectIds(tagUpdateDto.ProjectId, projectId);

        var tag = _mapper.Map<Tag>(tagUpdateDto);

        if (!await _tagRepository.UpdateAsync(tag))
        {
            _logger.LogError("Updating Tag failed. TagId: {TagId}", tag.Id);
            throw new OperationFailedException("Updating Tag failed.");
        }

        _logger.LogInformation("Updating Tag successful. TagId: {TagId}", tag.Id);
    }

    public async Task DeleteTagAsync(Guid tagId, Guid projectId)
    {
        _logger.LogInformation("Deleting Tag called. TagId: {TagId}, ProjectId: {ProjectId}", tagId, projectId);

        var tag = await _tagRepository.GetByIdAsync(tagId);

        if (tag is null)
        {
            _logger.LogError("Deleting Tag failed. Tag not found. TagId: {TagId}", tagId);
            throw new NotFoundException("Tag not found.");
        }

        // Validate if project access is allowed
        _projectAccessValidator.ValidateProjectIds(tag.ProjectId, projectId);

        if (!await _tagRepository.DeleteByIdAsync(tagId))
        {
            _logger.LogError("Deleting Tag failed. TagId: {TagId}", tagId);
            throw new OperationFailedException("Deleting Tag failed.");
        }

        _logger.LogInformation("Deleting Tag successful. TagId: {TagId}", tagId);
    }

    public async Task<TagDto> GetTagByIdAsync(Guid tagId, Guid projectId)
    {
        _logger.LogInformation("Getting Tag by Id called. TagId: {TagId}", tagId);

        var tag = await _tagRepository.GetByIdAsync(tagId);

        if (tag is null)
        {
            _logger.LogError("Getting Tag failed. Tag not found. TagId: {TagId}", tagId);
            throw new NotFoundException("Tag not found.");
        }

        // Validate if project access is allowed
        _projectAccessValidator.ValidateProjectIds(tag.ProjectId, projectId);

        _logger.LogInformation("Getting Tag successful. TagId: {TagId}", tagId);
        return _mapper.Map<TagDto>(tag);
    }

    public async Task<IEnumerable<TagDto>> GetAllTagsAsync()
    {
        _logger.LogInformation("Getting all Tags called.");

        var tags = await _tagRepository.GetAllAsync();

        _logger.LogInformation("Getting all Tags successful. Count: {Count}", tags.Count());
        return _mapper.Map<IEnumerable<TagDto>>(tags);
    }

    public async Task<IEnumerable<TagDto>> GetTagsByProjectIdAsync(Guid projectId)
    {
        _logger.LogInformation("Getting Tags by ProjectId called. ProjectId: {ProjectId}", projectId);

        var tags = await _tagRepository.GetAllByProjectIdAsync(projectId);

        _logger.LogInformation("Getting Tags by ProjectId successful. Count: {Count}", tags.Count());
        return _mapper.Map<IEnumerable<TagDto>>(tags);
    }

    public async Task<IEnumerable<TagDto>> GetTagsByTicketIdAsync(Guid ticketId)
    {
        _logger.LogInformation("Getting Tags by TicketId called. TicketId: {TicketId}", ticketId);

        var tags = await _tagRepository.GetAllByTicketIdAsync(ticketId);

        _logger.LogInformation("Getting Tags by TicketId successful. Count: {Count}", tags.Count());
        return _mapper.Map<IEnumerable<TagDto>>(tags);
    }

    public async Task DeleteByProjectIdAsync(Guid projectId, IDbTransaction transaction)
    {
        _logger.LogInformation("Deleting Tags by ProjectId called. ProjectId: {ProjectId}", projectId);

        if (! await _tagRepository.DeleteAllByProjectIdAsync(projectId, transaction))
        {
            _logger.LogWarning("No Tags found related to ProjectId: {ProjectId}", projectId);
            return;
        }

        _logger.LogInformation("Deleting Tags by ProjectId successful. ProjectId: {ProjectId}", projectId);
    }
}
