using System.Data;
using AutoMapper;
using Microsoft.Extensions.Logging;
using ProjectManager_01.Application.Contracts.Auth;
using ProjectManager_01.Application.Contracts.Repositories;
using ProjectManager_01.Application.Contracts.Services;
using ProjectManager_01.Application.DTOs.TicketRelations;
using ProjectManager_01.Application.Exceptions;
using ProjectManager_01.Domain.Models;

namespace ProjectManager_01.Application.Services;

public class TicketRelationService : ITicketRelationService
{
    private readonly ITicketRelationRepository _ticketRelationRepository;
    private readonly IProjectAccessValidator _projectAccessValidator;
    private readonly IMapper _mapper;
    private readonly ILogger<TicketRelationService> _logger;

    public TicketRelationService(
        ITicketRelationRepository ticketRelationRepository,
        IProjectAccessValidator projectAccessValidator,
        IMapper mapper,
        ILogger<TicketRelationService> logger)
    {
        _ticketRelationRepository = ticketRelationRepository;
        _projectAccessValidator = projectAccessValidator;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task CreateTicketRelationAsync(TicketRelationCreateDto ticketRelationDto, Guid projectId)
    {
        _logger.LogInformation("Creating TicketRelation called. SourceId: {SourceId}, TargetId: {TargetId}", ticketRelationDto.SourceId, ticketRelationDto.TargetId);

        // Validate if project access is allowed
        await _projectAccessValidator.ValidateTicketProjectIdAsync(ticketRelationDto.SourceId, projectId);
        await _projectAccessValidator.ValidateTicketProjectIdAsync(ticketRelationDto.TargetId, projectId);

        var relation = _mapper.Map<TicketRelation>(ticketRelationDto);
        relation.Id = Guid.NewGuid();

        if (!await _ticketRelationRepository.CreateAsync(relation))
        {
            _logger.LogError("Creating TicketRelation failed. SourceId: {SourceId}, TargetId: {TargetId}", ticketRelationDto.SourceId, ticketRelationDto.TargetId);
            throw new OperationFailedException("Creating TicketRelation failed.");
        }

        _logger.LogInformation("Creating TicketRelation successful. Id: {Id}", relation.Id);
    }

    public async Task UpdateTicketRelationAsync(TicketRelationUpdateDto ticketRelationDto, Guid projectId)
    {
        _logger.LogInformation("Updating TicketRelation called. Id: {Id}", ticketRelationDto.Id);

        // Validate if project access is allowed
        await _projectAccessValidator.ValidateTicketProjectIdAsync(ticketRelationDto.SourceId, projectId);
        await _projectAccessValidator.ValidateTicketProjectIdAsync(ticketRelationDto.TargetId, projectId);

        var relation = _mapper.Map<TicketRelation>(ticketRelationDto);

        if (!await _ticketRelationRepository.UpdateAsync(relation))
        {
            _logger.LogError("Updating TicketRelation failed. Id: {Id}", ticketRelationDto.Id);
            throw new OperationFailedException("Updating TicketRelation failed.");
        }

        _logger.LogInformation("Updating TicketRelation successful. Id: {Id}", ticketRelationDto.Id);
    }

    public async Task DeleteTicketRelationAsync(Guid ticketRelationId, Guid projectId)
    {
        _logger.LogInformation("Deleting TicketRelation called. Id: {Id}", ticketRelationId);

        var relation = await _ticketRelationRepository.GetByIdAsync(ticketRelationId);
        if (relation is null)
        {
            _logger.LogError("Deleting TicketRelation failed. Not found. Id: {Id}", ticketRelationId);
            throw new NotFoundException("TicketRelation not found.");
        }

        // Validate if project access is allowed
        await _projectAccessValidator.ValidateTicketProjectIdAsync(relation.SourceId, projectId);

        if (!await _ticketRelationRepository.DeleteByIdAsync(ticketRelationId))
        {
            _logger.LogError("Deleting TicketRelation failed. Id: {Id}", ticketRelationId);
            throw new OperationFailedException("Deleting TicketRelation failed.");
        }

        _logger.LogInformation("Deleting TicketRelation successful. Id: {Id}", ticketRelationId);
    }

    public async Task<TicketRelationDto> GetTicketRelationByIdAsync(Guid id, Guid projectId)
    {
        _logger.LogInformation("Getting TicketRelation by Id called. Id: {Id}", id);

        var relation = await _ticketRelationRepository.GetByIdAsync(id);
        if (relation is null)
        {
            _logger.LogError("Getting TicketRelation failed. Not found. Id: {Id}", id);
            throw new NotFoundException("TicketRelation not found.");
        }

        // Validate if project access is allowed
        await _projectAccessValidator.ValidateTicketProjectIdAsync(relation.SourceId, projectId);

        _logger.LogInformation("Getting TicketRelation successful. Id: {Id}", id);
        return _mapper.Map<TicketRelationDto>(relation);
    }

    public async Task<IEnumerable<TicketRelationDto>> GetAllTicketRelationsAsync()
    {
        _logger.LogInformation("Getting all TicketRelations called.");

        var relations = await _ticketRelationRepository.GetAllAsync();

        _logger.LogInformation("Getting all TicketRelations successful. Count: {Count}", relations.Count());
        return _mapper.Map<IEnumerable<TicketRelationDto>>(relations);
    }

    public async Task DeleteTicketRelationByTicketIdAsync(Guid ticketId, IDbTransaction transaction)
    {
        _logger.LogInformation("Deleting TicketRelations by TicketId called. TicketId: {TicketId}", ticketId);

        if (!await _ticketRelationRepository.DeleteAllByTicketIdAsync(ticketId, transaction))
        {
            _logger.LogError("Deleting TicketRelations by TicketId failed. TicketId: {TicketId}", ticketId);
            throw new OperationFailedException("Deleting TicketRelations failed.");
        }

        _logger.LogInformation("Deleting TicketRelations by TicketId successful. TicketId: {TicketId}", ticketId);
    }

    public async Task<IEnumerable<TicketRelationDto>> GetTicketRelationsBySourceIdAsync(Guid ticketId)
    {
        _logger.LogInformation("Getting TicketRelations by SourceId called. SourceId: {SourceId}", ticketId);

        var relations = await _ticketRelationRepository.GetAllBySourceIdAsync(ticketId);

        _logger.LogInformation("Getting TicketRelations by SourceId successful. Count: {Count}", relations.Count());
        return _mapper.Map<IEnumerable<TicketRelationDto>>(relations);
    }

    public async Task<IEnumerable<TicketRelationDto>> GetTicketRelationsByTargetIdAsync(Guid ticketId)
    {
        _logger.LogInformation("Getting TicketRelations by TargetId called. TargetId: {TargetId}", ticketId);

        var relations = await _ticketRelationRepository.GetAllByTargetIdAsync(ticketId);

        _logger.LogInformation("Getting TicketRelations by TargetId successful. Count: {Count}", relations.Count());
        return _mapper.Map<IEnumerable<TicketRelationDto>>(relations);
    }
}
