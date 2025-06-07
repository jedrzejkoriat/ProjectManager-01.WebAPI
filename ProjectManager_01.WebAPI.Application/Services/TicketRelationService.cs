using System.Data;
using AutoMapper;
using ProjectManager_01.Application.Contracts.Auth;
using ProjectManager_01.Application.Contracts.Repositories;
using ProjectManager_01.Application.Contracts.Services;
using ProjectManager_01.Application.DTOs.TicketRelations;
using ProjectManager_01.Domain.Models;

namespace ProjectManager_01.Application.Services;

public class TicketRelationService : ITicketRelationService
{
    private readonly ITicketRelationRepository _ticketRelationRepository;
    private readonly IProjectAccessValidator _projectValidatorHelper;
    private readonly IMapper _mapper;

    public TicketRelationService(
        ITicketRelationRepository ticketRelationRepository,
        IProjectAccessValidator projectValidatorHelper,
        IMapper mapper)
    {
        _ticketRelationRepository = ticketRelationRepository;
        _projectValidatorHelper = projectValidatorHelper;
        _mapper = mapper;
    }

    public async Task CreateTicketRelationAsync(TicketRelationCreateDto ticketRelationCreateDto, Guid projectId)
    {
        await _projectValidatorHelper.ValidateTicketProjectIdAsync(ticketRelationCreateDto.SourceId, projectId);
        var ticketRelation = _mapper.Map<TicketRelation>(ticketRelationCreateDto);
        await _ticketRelationRepository.CreateAsync(ticketRelation);
    }

    public async Task UpdateTicketRelationAsync(TicketRelationUpdateDto ticketRelationUpdateDto, Guid projectId)
    {
        await _projectValidatorHelper.ValidateTicketProjectIdAsync(ticketRelationUpdateDto.SourceId, projectId);
        var ticketRelation = _mapper.Map<TicketRelation>(ticketRelationUpdateDto);
        await _ticketRelationRepository.UpdateAsync(ticketRelation);
    }

    public async Task DeleteTicketRelationAsync(Guid ticketRelationId, Guid projectId)
    {
        await _projectValidatorHelper.ValidateTicketProjectIdAsync((await _ticketRelationRepository.GetByIdAsync(ticketRelationId)).SourceId, projectId);
        await _ticketRelationRepository.DeleteAsync(ticketRelationId);
    }

    public async Task<TicketRelationDto> GetTicketRelationByIdAsync(Guid ticketRelationId, Guid projectId)
    {
        var ticketRelation = await _ticketRelationRepository.GetByIdAsync(ticketRelationId);
        await _projectValidatorHelper.ValidateTicketProjectIdAsync(ticketRelation.SourceId, projectId);

        return _mapper.Map<TicketRelationDto>(ticketRelation);
    }

    public async Task<IEnumerable<TicketRelationDto>> GetAllTicketRelationsAsync()
    {
        var ticketRelations = await _ticketRelationRepository.GetAllAsync();

        return _mapper.Map<IEnumerable<TicketRelationDto>>(ticketRelations);
    }

    public async Task DeleteTicketRelationByTicketIdAsync(Guid ticketId, IDbTransaction transaction)
    {
        await _ticketRelationRepository.DeleteByTicketIdAsync(ticketId, transaction);
    }

    public async Task<IEnumerable<TicketRelationDto>> GetTicketRelationsBySourceIdAsync(Guid ticketId)
    {
        var ticketRelations = await _ticketRelationRepository.GetBySourceIdAsync(ticketId);

        return _mapper.Map<IEnumerable<TicketRelationDto>>(ticketRelations);
    }

    public async Task<IEnumerable<TicketRelationDto>> GetTicketRelationsByTargetIdAsync(Guid ticketId)
    {
        var ticketRelations = await _ticketRelationRepository.GetByTargetIdAsync(ticketId);

        return _mapper.Map<IEnumerable<TicketRelationDto>>(ticketRelations);
    }
}
