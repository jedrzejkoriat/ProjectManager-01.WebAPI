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
    private readonly ITicketRepository _ticketRepository;
    private readonly IMapper _mapper;
    private readonly IDbConnection _dbConnection;
    private readonly ITicketRelationService _ticketRelationService;
    private readonly ICommentService _commentService;
    private readonly ITicketTagService _ticketTagService;
    private readonly ITagService _tagService;

    public TicketService(
        ITicketRepository ticketRepository,
        IMapper mapper,
        IDbConnection dbConnection,
        ITicketRelationService ticketRelationService,
        ICommentService commentService,
        ITicketTagService ticketTagService,
        ITagService tagService)
    {
        _ticketRepository = ticketRepository;
        _mapper = mapper;
        _dbConnection = dbConnection;
        _ticketRelationService = ticketRelationService;
        _commentService = commentService;
        _ticketTagService = ticketTagService;
        _tagService = tagService;
    }

    public async Task CreateTicketAsync(TicketCreateDto ticketCreateDto)
    {
        using var transaction = DbTransactionHelper.BeginTransaction(_dbConnection);

        try
        {
            var ticket = _mapper.Map<Ticket>(ticketCreateDto);
            var ticketId = await _ticketRepository.CreateAsync(ticket, transaction);

            foreach (var tagId in ticketCreateDto.TagIds)
            {
                var ticketTag = new TicketTagCreateDto(tagId, ticketId);
                await _ticketTagService.CreateTicketTagAsync(ticketTag, transaction);
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
        var ticket = _mapper.Map<Ticket>(ticketUpdateDto);
        await _ticketRepository.UpdateAsync(ticket);
    }

    public async Task DeleteTicketAsync(Guid ticketId)
    {
        using var transaction = DbTransactionHelper.BeginTransaction(_dbConnection);

        try
        {
            await _ticketRelationService.DeleteTicketRelationByTicketIdAsync(ticketId, transaction);
            await _commentService.DeleteByTicketIdAsync(ticketId, transaction);

            await _ticketRepository.DeleteAsync(ticketId, transaction);

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
        await DeleteTicketsAsync(
            tr => _ticketRepository.GetByProjectIdAsync(projectId),
            tr => _ticketRepository.DeleteByProjectIdAsync(projectId, tr),
            transaction);
    }

    public async Task DeleteTicketByUserIdAsync(Guid userId, IDbTransaction transaction)
    {
        await DeleteTicketsAsync(
            tr => _ticketRepository.GetByReporterIdAsync(userId, tr),
            tr => _ticketRepository.DeleteByUserIdAsync(userId, tr),
            transaction);
    }

    public async Task DeleteTicketByPriorityIdAsync(Guid priorityId, IDbTransaction transaction)
    {
        await DeleteTicketsAsync(
            tr => _ticketRepository.GetByPriorityIdAsync(priorityId),
            tr => _ticketRepository.DeleteByPriorityIdAsync(priorityId, tr),
            transaction);
    }

    public async Task<TicketDto> GetTicketByIdAsync(Guid ticketId)
    {
        var ticket = await _ticketRepository.GetByIdAsync(ticketId);

        return await GetTicketDtoWithRelationsAsync(ticket);
    }

    public async Task<TicketDto> GetTicketByKeyAndNumberAsync(string projectKey, int ticketNumber)
    {
        var ticket = await _ticketRepository.GetByKeyAndNumberAsync(projectKey, ticketNumber);

        return await GetTicketDtoWithRelationsAsync(ticket);
    }

    public async Task<IEnumerable<TicketDto>> GetAllTicketsAsync()
    {
        var tickets = await _ticketRepository.GetAllAsync();

        return _mapper.Map<IEnumerable<TicketDto>>(tickets);
    }

    public async Task ClearUserAssignmentAsync(Guid userId, IDbTransaction transaction)
    {
        await _ticketRepository.ClearUserAssignmentAsync(userId, transaction);
    }

    public async Task SoftDeleteTicketAsync(Guid ticketId)
    {
        await _ticketRepository.SoftDeleteAsync(ticketId);
    }

    public async Task<IEnumerable<TicketDto>> GetTicketsByProjectIdAsync(Guid projectId)
    {
        var tickets = await _ticketRepository.GetByProjectIdAsync(projectId);

        return _mapper.Map<IEnumerable<TicketDto>>(tickets);
    }

    // Method to handle deletion of tickets and their related entities
    private async Task DeleteTicketsAsync(
        Func<IDbTransaction, Task<IEnumerable<Ticket>>> getTickets,
        Func<IDbTransaction, Task> deleteTickets,
        IDbTransaction transaction)
    {
        var tickets = await getTickets(transaction);

        foreach (var ticket in tickets)
        {
            await _ticketRelationService.DeleteTicketRelationByTicketIdAsync(ticket.Id, transaction);
            await _commentService.DeleteByTicketIdAsync(ticket.Id, transaction);
        }

        await deleteTickets(transaction);
    }

    // Method to handle getting ticket relations
    private async Task<TicketDto> GetTicketDtoWithRelationsAsync(Ticket ticket)
    {
        var ticketDto = _mapper.Map<TicketDto>(ticket);
        ticketDto.Comments = await _commentService.GetByTicketIdAsync(ticket.Id);
        ticketDto.Tags = await _tagService.GetTagsByTicketIdAsync(ticket.Id);
        ticketDto.RelationsAsSource = await _ticketRelationService.GetTicketRelationsBySourceIdAsync(ticket.Id);
        ticketDto.RelationsAsTarget = await _ticketRelationService.GetTicketRelationsByTargetIdAsync(ticket.Id);

        return ticketDto;
    }
}
