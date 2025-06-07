using System.Data;
using AutoMapper;
using Microsoft.Extensions.Logging;
using ProjectManager_01.Application.Contracts.Auth;
using ProjectManager_01.Application.Contracts.Repositories;
using ProjectManager_01.Application.Contracts.Services;
using ProjectManager_01.Application.DTOs.TicketTags;
using ProjectManager_01.Application.Exceptions;
using ProjectManager_01.Domain.Models;

namespace ProjectManager_01.Application.Services;

public class TicketTagService : ITicketTagService
{
    private readonly ITicketTagRepository _ticketTagRepository;
    private readonly IProjectAccessValidator _projectAccessValidator;
    private readonly IMapper _mapper;
    private readonly ILogger<TicketTagService> _logger;

    public TicketTagService(
        ITicketTagRepository ticketTagRepository,
        IProjectAccessValidator projectAccessValidator,
        IMapper mapper,
        ILogger<TicketTagService> logger)
    {
        _ticketTagRepository = ticketTagRepository;
        _projectAccessValidator = projectAccessValidator;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task CreateTicketTagAsync(TicketTagCreateDto ticketTagCreateDto, IDbTransaction dbTransaction)
    {
        _logger.LogWarning("Creating TicketTag called. TagId: {TagId}, TicketId: {TicketId}", ticketTagCreateDto.TagId, ticketTagCreateDto.TicketId);

        var ticketTag = _mapper.Map<TicketTag>(ticketTagCreateDto);

        // Check if operation is successful
        if (!await _ticketTagRepository.CreateAsync(ticketTag, dbTransaction))
        {
            _logger.LogError("Creating TicketTag failed. TagId: {TagId}, TicketId: {TicketId}", ticketTagCreateDto.TagId, ticketTagCreateDto.TicketId);
            throw new OperationFailedException("Creating TicketTag failed.");
        }

        _logger.LogInformation("Creating TicketTag successful. TagId: {TagId}, TicketId: {TicketId}", ticketTagCreateDto.TagId, ticketTagCreateDto.TicketId);
    }

    public async Task CreateTicketTagAsync(TicketTagCreateDto ticketTagCreateDto, Guid projectId)
    {
        _logger.LogWarning("Creating TicketTag called. TagId: {TagId}, TicketId: {TicketId}", ticketTagCreateDto.TagId, ticketTagCreateDto.TicketId);

        await _projectAccessValidator.ValidateTagProjectIdAsync(ticketTagCreateDto.TagId, projectId);
        var ticketTag = _mapper.Map<TicketTag>(ticketTagCreateDto);

        // Check if operation is successful
        if (!await _ticketTagRepository.CreateAsync(ticketTag))
        {
            _logger.LogError("Creating TicketTag failed. TagId: {TagId}, TicketId: {TicketId}", ticketTagCreateDto.TagId, ticketTagCreateDto.TicketId);
            throw new OperationFailedException("Creating TicketTag failed.");
        }

        _logger.LogInformation("Creating TicketTag successful. TagId: {TagId}, TicketId: {TicketId}", ticketTagCreateDto.TagId, ticketTagCreateDto.TicketId);
    }

    public async Task DeleteTicketTagAsync(Guid ticketId, Guid tagId, Guid projectId)
    {
        _logger.LogWarning("Deleting TicketTag called. TagId: {TagId}, TicketId: {TicketId}", tagId, ticketId);

        await _projectAccessValidator.ValidateTagProjectIdAsync(tagId, projectId);

        // Check if operation is successful
        if (!await _ticketTagRepository.DeleteByIdAsync(ticketId, tagId))
        {
            _logger.LogError("Deleting TicketTag failed. TagId: {TagId}, TicketId: {TicketId}", tagId, ticketId);
            throw new OperationFailedException("Deleting TicketTag failed.");
        }

        _logger.LogInformation("Deleting TicketTag successful. TagId: {TagId}, TicketId: {TicketId}", tagId, ticketId);
    }

    public async Task<TicketTagDto> GetTicketTagByIdAsync(Guid ticketId, Guid tagId, Guid projectId)
    {
        _logger.LogInformation("Getting TicketTag called. TagId: {TagId}, TicketId: {TicketId}", tagId, ticketId);

        await _projectAccessValidator.ValidateTagProjectIdAsync(tagId, projectId);
        var ticketTag = await _ticketTagRepository.GetByIdAsync(ticketId, tagId);

        // Check if operation is successful
        if (ticketTag == null)
        {
            _logger.LogError("Getting TicketTag failed. TagId: {TagId}, TicketId: {TicketId}", tagId, ticketId);
            throw new NotFoundException("TicketTag not found.");
        }

        _logger.LogInformation("Getting TicketTag successful. TagId: {TagId}, TicketId: {TicketId}", tagId, ticketId);
        return _mapper.Map<TicketTagDto>(ticketTag);
    }

    public async Task<IEnumerable<TicketTagDto>> GetAllTicketTagsAsync()
    {
        _logger.LogInformation("Getting all TicketTags called.");

        var ticketTags = await _ticketTagRepository.GetAllAsync();

        _logger.LogInformation("Getting all TicketTags successful. Count: {Count}", ticketTags.Count());
        return _mapper.Map<IEnumerable<TicketTagDto>>(ticketTags);
    }
}
