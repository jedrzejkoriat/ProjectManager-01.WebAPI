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

    public TicketRelationService(ITicketRelationRepository ticketRelationRepository, IMapper mapper)
    {
        this.ticketRelationRepository = ticketRelationRepository;
        this.mapper = mapper;
    }

    public async Task CreateTicketRelationAsync(TicketRelationCreateDto ticketRelationCreateDto)
    {
        TicketRelation ticketRelation = mapper.Map<TicketRelation>(ticketRelationCreateDto);
        await ticketRelationRepository.CreateAsync(ticketRelation);
    }

    public async Task UpdateTicketRelationAsync(TicketRelationUpdateDto ticketRelationUpdateDto)
    {
        TicketRelation ticketRelation = mapper.Map<TicketRelation>(ticketRelationUpdateDto);
        await ticketRelationRepository.UpdateAsync(ticketRelation);
    }

    public async Task DeleteTicketRelationAsync(Guid ticketRelationId)
    {
        await ticketRelationRepository.DeleteAsync(ticketRelationId);
    }

    public async Task<TicketRelationDto> GetTicketRelationByIdAsync(Guid ticketRelationId)
    {
        TicketRelation ticketRelation = await ticketRelationRepository.GetByIdAsync(ticketRelationId);

        return mapper.Map<TicketRelationDto>(ticketRelation);
    }

    public async Task<List<TicketRelationDto>> GetAllTicketRelationsAsync()
    {
        List<TicketRelation> ticketRelations = await ticketRelationRepository.GetAllAsync();

        return mapper.Map<List<TicketRelationDto>>(ticketRelations);
    }
}
