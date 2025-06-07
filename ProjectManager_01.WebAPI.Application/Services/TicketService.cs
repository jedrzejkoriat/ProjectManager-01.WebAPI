using System.Data;
using System.Net.Sockets;
using AutoMapper;
using ProjectManager_01.Application.Contracts.Auth;
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
    private readonly IProjectAccessValidator _projectAccessValidator;
    private readonly ITagService _tagService;

    public TicketService(
        ITicketRepository ticketRepository,
        IMapper mapper,
        IDbConnection dbConnection,
        ITicketRelationService ticketRelationService,
        ICommentService commentService,
        ITicketTagService ticketTagService,
        IProjectAccessValidator projectAccessValidator,
        ITagService tagService)
    {
        _ticketRepository = ticketRepository;
        _mapper = mapper;
        _dbConnection = dbConnection;
        _ticketRelationService = ticketRelationService;
        _commentService = commentService;
        _ticketTagService = ticketTagService;
        _projectAccessValidator = projectAccessValidator;
        _tagService = tagService;
    }

    public async Task CreateTicketAsync(TicketCreateDto ticketCreateDto, Guid projectId)
    {
        _projectAccessValidator.ValidateProjectIds(ticketCreateDto.ProjectId, projectId);

        var projectTickets = await _ticketRepository.GetAllByProjectIdAsync(ticketCreateDto.ProjectId);

        using var transaction = DbTransactionHelper.BeginTransaction(_dbConnection);

        try
        {
            var ticket = _mapper.Map<Ticket>(ticketCreateDto);
            ticket.TicketNumber = projectTickets.Any() ? projectTickets.Max(t => t.TicketNumber) + 1 : 1;

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

    public async Task<TicketDto> UpdateTicketAsync(TicketUpdateDto ticketUpdateDto, Guid projectId)
    {
        _projectAccessValidator.ValidateProjectIds(ticketUpdateDto.ProjectId, projectId);

        var ticket = _mapper.Map<Ticket>(ticketUpdateDto);
        await _ticketRepository.UpdateAsync(ticket);

        ticket = await _ticketRepository.GetByIdAsync(ticket.Id);
        return await GetTicketDtoWithRelationsAsync(ticket);
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
            tr => _ticketRepository.GetAllByProjectIdAsync(projectId),
            tr => _ticketRepository.DeleteAllByProjectIdAsync(projectId, tr),
            transaction);
    }

    public async Task DeleteTicketByUserIdAsync(Guid userId, IDbTransaction transaction)
    {
        await DeleteTicketsAsync(
            tr => _ticketRepository.GetAllByReporterIdAsync(userId, tr),
            tr => _ticketRepository.DeleteAllByUserIdAsync(userId, tr),
            transaction);
    }

    public async Task DeleteTicketByPriorityIdAsync(Guid priorityId, IDbTransaction transaction)
    {
        await DeleteTicketsAsync(
            tr => _ticketRepository.GetAllByPriorityIdAsync(priorityId),
            tr => _ticketRepository.DeleteAllByPriorityIdAsync(priorityId, tr),
            transaction);
    }

    public async Task<TicketDto> GetTicketByIdAsync(Guid ticketId, Guid projectId)
    {
        var ticket = await _ticketRepository.GetByIdAsync(ticketId);

        _projectAccessValidator.ValidateProjectIds(ticket.ProjectId, projectId);

        return await GetTicketDtoWithRelationsAsync(ticket);
    }

    public async Task<TicketDto> GetTicketByKeyAndNumberAsync(string projectKey, int ticketNumber, Guid projectId)
    {
        var ticket = await _ticketRepository.GetByProjectKeyAndTicketNumberAsync(projectKey, ticketNumber);

        _projectAccessValidator.ValidateProjectIds(ticket.ProjectId, projectId);

        return await GetTicketDtoWithRelationsAsync(ticket);
    }

    public async Task<IEnumerable<TicketDto>> GetAllTicketsAsync()
    {
        var tickets = await _ticketRepository.GetAllAsync();

        return _mapper.Map<IEnumerable<TicketDto>>(tickets);
    }

    public async Task ClearUserAssignmentAsync(Guid userId, IDbTransaction transaction)
    {
        await _ticketRepository.ClearUserAssignmentsAsync(userId, transaction);
    }

    public async Task SoftDeleteTicketAsync(Guid ticketId, Guid projectId)
    {
        await _projectAccessValidator.ValidateTicketProjectIdAsync(ticketId, projectId);

        await _ticketRepository.SoftDeleteByIdAsync(ticketId);
    }

    public async Task<IEnumerable<TicketDto>> GetTicketsByProjectIdAsync(Guid projectId)
    {
        var tickets = await _ticketRepository.GetAllByProjectIdAsync(projectId);

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
