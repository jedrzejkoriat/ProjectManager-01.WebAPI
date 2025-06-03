using System.Data;
using System.Transactions;
using AutoMapper;
using Microsoft.Data.SqlClient;
using ProjectManager_01.Application.Contracts.Factories;
using ProjectManager_01.Application.Contracts.Repositories;
using ProjectManager_01.Application.Contracts.Services;
using ProjectManager_01.Application.DTOs.Tickets;
using ProjectManager_01.Domain.Models;

namespace ProjectManager_01.Application.Services;

public class TicketService : ITicketService
{

    private readonly ITicketRepository ticketRepository;
    private readonly IMapper mapper;
    private readonly IDbConnectionFactory dbConnectionFactory;
    private readonly ITicketRelationService ticketRelationService;
    private readonly ICommentService commentService;

    public TicketService(
        ITicketRepository ticketRepository, 
        IMapper mapper, 
        IDbConnectionFactory dbConnectionFactory,
        ITicketRelationService ticketRelationService,
        ICommentService commentService)
    {
        this.ticketRepository = ticketRepository;
        this.mapper = mapper;
        this.dbConnectionFactory = dbConnectionFactory;
        this.ticketRelationService = ticketRelationService;
        this.commentService = commentService;
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
        using var connection = dbConnectionFactory.CreateConnection();

        switch (connection)
        {
            case SqlConnection sqlConnection:
                await sqlConnection.OpenAsync();
                break;
            default:
                connection.Open();
                break;
        }

        using var transaction = connection.BeginTransaction();

        try
        {
            await ticketRepository.DeleteAsync(ticketId, connection, transaction);
            await ticketRelationService.DeleteTicketRelationByTicketIdAsync(ticketId, connection, transaction);
            await commentService.DeleteByTicketIdAsync(ticketId, connection, transaction);

            transaction.Commit();
        }
        catch
        {
            transaction.Rollback();
            throw new Exception("Error while performing ticket deletion transaction.");
        }

    }

    public async Task DeleteByProjectIdAsync(Guid projectId, IDbConnection connection, IDbTransaction transaction)
    {
        var tickets = await ticketRepository.GetByProjectIdAsync(projectId);

        foreach (var ticket in tickets)
        {
            await ticketRelationService.DeleteTicketRelationByTicketIdAsync(ticket.Id, connection, transaction);
            await commentService.DeleteByTicketIdAsync(ticket.Id, connection, transaction);
        }

        await ticketRepository.DeleteByProjectIdAsync(projectId, connection, transaction);
    }

    public async Task DeleteTicketByUserIdAsync(Guid userId, IDbConnection connection, IDbTransaction transaction)
    {
        var tickets = await ticketRepository.GetByReporterIdAsync(userId, connection, transaction);

        foreach (var ticket in tickets)
        {
            await ticketRelationService.DeleteTicketRelationByTicketIdAsync(ticket.Id, connection, transaction);
            await commentService.DeleteByTicketIdAsync(ticket.Id, connection, transaction);
        }

        await ticketRepository.DeleteByUserIdAsync(userId, connection, transaction);
    }

    public async Task DeleteTicketByPriorityIdAsync(Guid priorityId, IDbConnection connection, IDbTransaction transaction)
    {
        var tickets = await ticketRepository.GetByPriorityAsync(priorityId);

        foreach (var ticket in tickets)
        {
            await ticketRelationService.DeleteTicketRelationByTicketIdAsync(ticket.Id, connection, transaction);
            await commentService.DeleteByTicketIdAsync(ticket.Id, connection, transaction);
        }

        await ticketRepository.DeleteByPriorityIdAsync(priorityId, connection, transaction);
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

    public async Task ClearUserAssignmentAsync(Guid userId, IDbConnection connection, IDbTransaction transaction)
    {
        await ticketRepository.ClearUserAssignmentAsync(userId, connection, transaction);
    }

    public async Task SoftDeleteTicketAsync(Guid ticketId)
    {
        await ticketRepository.SoftDeleteAsync(ticketId);
    }
}
