using System.Data;
using AutoMapper;
using ProjectManager_01.Application.Contracts.Repositories;
using ProjectManager_01.Application.Contracts.Services;
using ProjectManager_01.Application.DTOs.Tickets;
using ProjectManager_01.Application.DTOs.TicketTags;
using ProjectManager_01.Application.Helpers;
using ProjectManager_01.Domain.Models;

namespace ProjectManager_01.Application.Services;

public class TicketService : ITicketService
{
    private readonly ITicketRepository ticketRepository;
    private readonly IMapper mapper;
    private readonly IDbConnection dbConnection;
    private readonly ITicketRelationService ticketRelationService;
    private readonly ICommentService commentService;
    private readonly ITicketTagService ticketTagService;
    private readonly ITagService tagService;

    public TicketService(
        ITicketRepository ticketRepository,
        IMapper mapper,
        IDbConnection dbConnection,
        ITicketRelationService ticketRelationService,
        ICommentService commentService,
        ITicketTagService ticketTagService,
        ITagService tagService)
    {
        this.ticketRepository = ticketRepository;
        this.mapper = mapper;
        this.dbConnection = dbConnection;
        this.ticketRelationService = ticketRelationService;
        this.commentService = commentService;
        this.ticketTagService = ticketTagService;
        this.tagService = tagService;
    }

    public async Task CreateTicketAsync(TicketCreateDto ticketCreateDto)
    {
        using var transaction = DbTransactionHelper.BeginTransaction(dbConnection);

        try
        {
            var ticket = mapper.Map<Ticket>(ticketCreateDto);
            var ticketId = await ticketRepository.CreateAsync(ticket, transaction);

            foreach (var tagId in ticketCreateDto.TagIds)
            {
                var ticketTag = new TicketTagCreateDto(tagId, ticketId);
                await ticketTagService.CreateTicketTagAsync(ticketTag, transaction);
            }

            transaction.Commit();
        }
        catch
        {
            transaction.Rollback();
            throw new Exception("Error while performing ticket creation transaction.");
        }
    }

    public async Task UpdateTicketAsync(TicketUpdateDto ticketUpdateDto)
    {
        var ticket = mapper.Map<Ticket>(ticketUpdateDto);
        await ticketRepository.UpdateAsync(ticket);
    }

    public async Task DeleteTicketAsync(Guid ticketId)
    {
        using var transaction = DbTransactionHelper.BeginTransaction(dbConnection);

        try
        {
            await ticketRepository.DeleteAsync(ticketId, transaction);
            await ticketRelationService.DeleteTicketRelationByTicketIdAsync(ticketId, transaction);
            await commentService.DeleteByTicketIdAsync(ticketId, transaction);

            transaction.Commit();
        }
        catch
        {
            transaction.Rollback();
            throw new Exception("Error while performing ticket deletion transaction.");
        }

    }

    public async Task DeleteByProjectIdAsync(Guid projectId, IDbTransaction transaction)
    {
        var tickets = await ticketRepository.GetByProjectIdAsync(projectId);

        foreach (var ticket in tickets)
        {
            await ticketRelationService.DeleteTicketRelationByTicketIdAsync(ticket.Id, transaction);
            await commentService.DeleteByTicketIdAsync(ticket.Id, transaction);
        }

        await ticketRepository.DeleteByProjectIdAsync(projectId, transaction);
    }

    public async Task DeleteTicketByUserIdAsync(Guid userId, IDbTransaction transaction)
    {
        var tickets = await ticketRepository.GetByReporterIdAsync(userId, transaction);

        foreach (var ticket in tickets)
        {
            await ticketRelationService.DeleteTicketRelationByTicketIdAsync(ticket.Id, transaction);
            await commentService.DeleteByTicketIdAsync(ticket.Id, transaction);
        }

        await ticketRepository.DeleteByUserIdAsync(userId, transaction);
    }

    public async Task DeleteTicketByPriorityIdAsync(Guid priorityId, IDbTransaction transaction)
    {
        var tickets = await ticketRepository.GetByPriorityIdAsync(priorityId);

        foreach (var ticket in tickets)
        {
            await ticketRelationService.DeleteTicketRelationByTicketIdAsync(ticket.Id, transaction);
            await commentService.DeleteByTicketIdAsync(ticket.Id, transaction);
        }

        await ticketRepository.DeleteByPriorityIdAsync(priorityId, transaction);
    }

    public async Task<TicketDto> GetTicketByIdAsync(Guid ticketId)
    {
        var ticket = await ticketRepository.GetByIdAsync(ticketId);

        var ticketDto = mapper.Map<TicketDto>(ticket);
        ticketDto.Comments = await commentService.GetByTicketIdAsync(ticketId);
        ticketDto.Tags = await tagService.GetTagsByTicketIdAsync(ticketId);
        ticketDto.RelationsAsSource = await ticketRelationService.GetTicketRelationsBySourceIdAsync(ticketId);
        ticketDto.RelationsAsTarget = await ticketRelationService.GetTicketRelationsByTargetIdAsync(ticketId);

        return ticketDto;
    }

    public async Task<IEnumerable<TicketDto>> GetAllTicketsAsync()
    {
        var tickets = await ticketRepository.GetAllAsync();

        return mapper.Map<IEnumerable<TicketDto>>(tickets);
    }

    public async Task ClearUserAssignmentAsync(Guid userId, IDbTransaction transaction)
    {
        await ticketRepository.ClearUserAssignmentAsync(userId, transaction);
    }

    public async Task SoftDeleteTicketAsync(Guid ticketId)
    {
        await ticketRepository.SoftDeleteAsync(ticketId);
    }

    public async Task<TicketDto> GetTicketByKeyAndNumberAsync(string projectKey, int ticketNumber)
    {
        var ticket = await ticketRepository.GetByKeyAndNumberAsync(projectKey, ticketNumber);

        return mapper.Map<TicketDto>(ticket);
    }

    public async Task<IEnumerable<TicketDto>> GetTicketsByProjectIdAsync(Guid projectId)
    {
        var tickets = await ticketRepository.GetByProjectIdAsync(projectId);

        return mapper.Map<IEnumerable<TicketDto>>(tickets);
    }
}
