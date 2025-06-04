using System.Data;
using AutoMapper;
using ProjectManager_01.Application.Contracts.Repositories;
using ProjectManager_01.Application.Contracts.Services;
using ProjectManager_01.Application.DTOs.TicketRelations;
using ProjectManager_01.Domain.Models;

namespace ProjectManager_01.Application.Services;

public class TicketRelationService : ITicketRelationService
{
    private readonly ITicketRelationRepository ticketRelationRepository;
    private readonly IMapper mapper;

    public TicketRelationService(
        ITicketRelationRepository ticketRelationRepository,
        IMapper mapper)
    {
        this.ticketRelationRepository = ticketRelationRepository;
        this.mapper = mapper;
    }

    public async Task CreateTicketRelationAsync(TicketRelationCreateDto ticketRelationCreateDto)
    {
        var ticketRelation = mapper.Map<TicketRelation>(ticketRelationCreateDto);
        await ticketRelationRepository.CreateAsync(ticketRelation);
    }

    public async Task UpdateTicketRelationAsync(TicketRelationUpdateDto ticketRelationUpdateDto)
    {
        var ticketRelation = mapper.Map<TicketRelation>(ticketRelationUpdateDto);
        await ticketRelationRepository.UpdateAsync(ticketRelation);
    }

    public async Task DeleteTicketRelationAsync(Guid ticketRelationId)
    {
        await ticketRelationRepository.DeleteAsync(ticketRelationId);
    }

    public async Task<TicketRelationDto> GetTicketRelationByIdAsync(Guid ticketRelationId)
    {
        var ticketRelation = await ticketRelationRepository.GetByIdAsync(ticketRelationId);

        return mapper.Map<TicketRelationDto>(ticketRelation);
    }

    public async Task<IEnumerable<TicketRelationDto>> GetAllTicketRelationsAsync()
    {
        var ticketRelations = await ticketRelationRepository.GetAllAsync();

        return mapper.Map<IEnumerable<TicketRelationDto>>(ticketRelations);
    }

    public async Task DeleteTicketRelationByTicketIdAsync(Guid ticketId, IDbTransaction transaction)
    {
        await ticketRelationRepository.DeleteByTicketIdAsync(ticketId, transaction);
    }

    public async Task<IEnumerable<TicketRelationDto>> GetTicketRelationsBySourceIdAsync(Guid ticketId)
    {
        var ticketRelations = await ticketRelationRepository.GetBySourceIdAsync(ticketId);

        return mapper.Map<IEnumerable<TicketRelationDto>>(ticketRelations);
    }

    public async Task<IEnumerable<TicketRelationDto>> GetTicketRelationsByTargetIdAsync(Guid ticketId)
    {
        var ticketRelations = await ticketRelationRepository.GetByTargetIdAsync(ticketId);

        return mapper.Map<IEnumerable<TicketRelationDto>>(ticketRelations);
    }
}
