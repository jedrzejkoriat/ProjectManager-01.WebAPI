using System.Data;
using System.Transactions;
using AutoMapper;
using ProjectManager_01.Application.Contracts.Repositories;
using ProjectManager_01.Application.Contracts.Services;
using ProjectManager_01.Application.DTOs.Tickets;
using ProjectManager_01.Domain.Models;

namespace ProjectManager_01.Application.Services;

public class TicketService : ITicketService
{

    private readonly ITicketRepository ticketRepository;
    private readonly IMapper mapper;

    public TicketService(ITicketRepository ticketRepository, IMapper mapper)
    {
        this.ticketRepository = ticketRepository;
        this.mapper = mapper;
    }

    public async Task DeleteByProjectIdAsync(Guid projectId, IDbConnection connection, IDbTransaction transaction)
    {
        await ticketRepository.GetByProjectIdAsync(projectId);
        await ticketRepository.DeleteByProjectIdAsync(projectId, connection, transaction);
    }

    public async Task CreateTicketAsync(TicketCreateDto ticketCreateDto)
    {
        Ticket ticket = mapper.Map<Ticket>(ticketCreateDto);
        await ticketRepository.CreateAsync(ticket);
    }

    public async Task UpdateTicketAsync(TicketUpdateDto ticketUpdateDto)
    {
        Ticket ticket = mapper.Map<Ticket>(ticketUpdateDto);
        await ticketRepository.UpdateAsync(ticket);
    }

    public async Task DeleteTicketAsync(Guid ticketId)
    {
        await ticketRepository.DeleteAsync(ticketId);
    }

    public async Task<TicketDto> GetTicketByIdAsync(Guid ticketId)
    {
        Ticket ticket = await ticketRepository.GetByIdAsync(ticketId);

        return mapper.Map<TicketDto>(ticket);
    }

    public async Task<List<TicketDto>> GetAllTicketsAsync()
    {
        List<Ticket> tickets = await ticketRepository.GetAllAsync();

        return mapper.Map<List<TicketDto>>(tickets);
    }

    public async Task ClearUserReferencesAsync(Guid userId, IDbConnection connection, IDbTransaction transaction)
    {
        await ticketRepository.ClearUserReferencesAsync(userId, connection, transaction);
    }

    public async Task SoftDeleteTicketAsync(Guid ticketId)
    {
        await ticketRepository.SoftDeleteAsync(ticketId);
    }
}
