using System.Data;
using System.Net.Sockets;
using AutoMapper;
using Microsoft.Extensions.Logging;
using ProjectManager_01.Application.Contracts.Auth;
using ProjectManager_01.Application.Contracts.Repositories;
using ProjectManager_01.Application.Contracts.Services;
using ProjectManager_01.Application.DTOs.Tickets;
using ProjectManager_01.Application.DTOs.TicketTags;
using ProjectManager_01.Application.Helpers;
using ProjectManager_01.Application.Exceptions;
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
    private readonly ILogger<TicketService> _logger;

    public TicketService(
        ITicketRepository ticketRepository,
        IMapper mapper,
        IDbConnection dbConnection,
        ITicketRelationService ticketRelationService,
        ICommentService commentService,
        ITicketTagService ticketTagService,
        IProjectAccessValidator projectAccessValidator,
        ITagService tagService,
        ILogger<TicketService> logger)
    {
        _ticketRepository = ticketRepository;
        _mapper = mapper;
        _dbConnection = dbConnection;
        _ticketRelationService = ticketRelationService;
        _commentService = commentService;
        _ticketTagService = ticketTagService;
        _projectAccessValidator = projectAccessValidator;
        _tagService = tagService;
        _logger = logger;
    }

    public async Task CreateTicketAsync(TicketCreateDto ticketCreateDto, Guid projectId)
    {
        _logger.LogWarning("Creating Ticket called. Project: {ProjectId}, User: {UserId}", ticketCreateDto.ProjectId, ticketCreateDto.ReporterId);

        // Validate if project access is allowed
        _projectAccessValidator.ValidateProjectIds(ticketCreateDto.ProjectId, projectId);

        var projectTickets = await _ticketRepository.GetAllByProjectIdAsync(ticketCreateDto.ProjectId);

        using var transaction = DbTransactionHelper.BeginTransaction(_dbConnection);

        try
        {
            var ticket = _mapper.Map<Ticket>(ticketCreateDto);
            ticket.TicketNumber = projectTickets.Any() ? projectTickets.Max(t => t.TicketNumber) + 1 : 1;
            ticket.Id = Guid.NewGuid();

            // Check if operation is successful
            if (!await _ticketRepository.CreateAsync(ticket, transaction))
            {
                _logger.LogError("Creating Ticket failed. Project: {ProjectId}, User: {UserId}", ticketCreateDto.ProjectId, ticketCreateDto.ReporterId);
                throw new OperationFailedException("Creating Ticket failed.");
            }

            foreach (var tagId in ticketCreateDto.TagIds)
            {
                var ticketTag = new TicketTagCreateDto(tagId, ticket.Id);
                await _ticketTagService.CreateTicketTagAsync(ticketTag, transaction);
            }

            transaction.Commit();
            _logger.LogInformation("Creating Ticket successful. Ticket: {TicketId}", ticket.Id);
        }
        catch
        {
            transaction.Rollback();
            _logger.LogError("Creating Ticket transaction failed. Ticket: Project: {ProjectId}, User: {UserId}", ticketCreateDto.ProjectId, ticketCreateDto.ReporterId);
            throw new Exception("Creating Ticket failed.");
        }
    }

    public async Task<TicketDto> UpdateTicketAsync(TicketUpdateDto ticketUpdateDto, Guid projectId)
    {
        _logger.LogInformation("Updating Ticket called. Ticket: {TicketId}", ticketUpdateDto.Id);

        // Validate if project access is allowed
        _projectAccessValidator.ValidateProjectIds(ticketUpdateDto.ProjectId, projectId);

        var ticket = _mapper.Map<Ticket>(ticketUpdateDto);

        // Check if operation is successful
        if (!await _ticketRepository.UpdateAsync(ticket))
        {
            _logger.LogError("Updating Ticket failed. Ticket: {TicketId}", ticket.Id);
            throw new OperationFailedException("Updating Ticket failed.");
        }

        ticket = await _ticketRepository.GetByIdAsync(ticket.Id);

        _logger.LogInformation("Updating Ticket successful. Ticket: {TicketId}", ticket.Id);
        return await GetTicketDtoWithRelationsAsync(ticket);
    }

    public async Task DeleteTicketAsync(Guid ticketId)
    {
        _logger.LogWarning("Deleting Ticket transaction called. Ticket: {TicketId}", ticketId);

        using var transaction = DbTransactionHelper.BeginTransaction(_dbConnection);

        try
        {
            await _ticketRelationService.DeleteTicketRelationByTicketIdAsync(ticketId, transaction);
            await _commentService.DeleteByTicketIdAsync(ticketId, transaction);

            // Check if operation is successful
            if (!await _ticketRepository.DeleteAsync(ticketId, transaction))
            {
                _logger.LogError("Deleting Ticket failed. Ticket: {TicketId}", ticketId);
                throw new OperationFailedException("Deleting Ticket transaction failed.");
            }

            transaction.Commit();
            _logger.LogInformation("Deleting Ticket transaction successful. Ticket: {TicketId}", ticketId);
        }
        catch
        {
            transaction.Rollback();
            _logger.LogError("Deleting Ticket transaction failed. Ticket: {TicketId}", ticketId);
            throw new Exception("Error while performing ticket deletion transaction.");
        }
    }

    public async Task<TicketDto> GetTicketByIdAsync(Guid ticketId, Guid projectId)
    {
        _logger.LogInformation("Getting Ticket called. Ticket: {TicketId}", ticketId);

        var ticket = await _ticketRepository.GetByIdAsync(ticketId);

        // Check if operation is successful
        if (ticket == null)
        {
            _logger.LogError("Getting Ticket failed. Ticket: {TicketId}", ticketId);
            throw new NotFoundException("Ticket not found.");
        }

        // Validate if project access is allowed
        _projectAccessValidator.ValidateProjectIds(ticket.ProjectId, projectId);

        _logger.LogInformation("Getting Ticket successful. Ticket: {TicketId}", ticketId);
        return await GetTicketDtoWithRelationsAsync(ticket);
    }

    public async Task<TicketDto> GetTicketByKeyAndNumberAsync(string projectKey, int ticketNumber, Guid projectId)
    {
        _logger.LogInformation("Getting Ticket by key called. Ticket: {ProjectKey}-{TicketNumber}", projectKey, ticketNumber);

        var ticket = await _ticketRepository.GetByProjectKeyAndTicketNumberAsync(projectKey, ticketNumber);

        // Check if operation is successful
        if (ticket == null)
        {
            _logger.LogError("Getting Ticket by key failed. Ticket: {ProjectKey}-{TicketNumber}", projectKey, ticketNumber);
            throw new NotFoundException("Ticket not found.");
        }

        // Validate if project access is allowed
        _projectAccessValidator.ValidateProjectIds(ticket.ProjectId, projectId);

        _logger.LogInformation("Getting Ticket by key successful. Ticket: {TicketId}", ticket.Id);
        return await GetTicketDtoWithRelationsAsync(ticket);
    }

    public async Task<IEnumerable<TicketOverviewDto>> GetAllTicketsAsync()
    {
        _logger.LogInformation("Getting all Tickets called.");

        var tickets = await _ticketRepository.GetAllAsync();

        _logger.LogInformation("Getting all Tickets successful. Count: {Count}", tickets.Count());
        return _mapper.Map<IEnumerable<TicketOverviewDto>>(tickets);
    }

    public async Task<IEnumerable<TicketOverviewDto>> GetTicketsByProjectIdAsync(Guid projectId)
    {
        _logger.LogInformation("Getting Tickets by project called. ProjectId: {ProjectId}", projectId);

        var tickets = await _ticketRepository.GetAllByProjectIdAsync(projectId);

        _logger.LogInformation("Getting Tickets by project successful. Count: {Count}", tickets.Count());
        return _mapper.Map<IEnumerable<TicketOverviewDto>>(tickets);
    }

    public async Task SoftDeleteTicketAsync(Guid ticketId, Guid projectId)
    {
        _logger.LogWarning("Soft deleting Ticket called. Ticket: {TicketId}", ticketId);

        // Validate if project access is allowed
        await _projectAccessValidator.ValidateTicketProjectIdAsync(ticketId, projectId);

        // Check if operation is successful
        if (!await _ticketRepository.SoftDeleteByIdAsync(ticketId))
        {
            _logger.LogError("Soft deleting Ticket failed. Ticket: {TicketId}", ticketId);
            throw new OperationFailedException("Soft deleting Ticket failed.");
        }

        _logger.LogInformation("Soft deleting Ticket successful. Ticket: {TicketId}", ticketId);
    }

    public async Task ClearUserAssignmentAsync(Guid userId, IDbTransaction transaction)
    {
        _logger.LogInformation("Clearing user assignments called. UserId: {UserId}", userId);

        if (!await _ticketRepository.ClearUserAssignmentsAsync(userId, transaction))
        {
            _logger.LogError("Clearing user assignments failed. UserId: {UserId}", userId);
            throw new OperationFailedException("Clearing user assignments failed.");
        }

        _logger.LogInformation("Clearing user assignments successful. UserId: {UserId}", userId);
    }

    public async Task DeleteByProjectIdAsync(Guid projectId, IDbTransaction transaction)
    {
        _logger.LogWarning("Deleting Tickets by project called. ProjectId: {ProjectId}", projectId);

        await DeleteTicketsAsync(
            tr => _ticketRepository.GetAllByProjectIdAsync(projectId, tr),
            tr => _ticketRepository.DeleteAllByProjectIdAsync(projectId, tr),
            transaction);

        _logger.LogInformation("Deleting Tickets by project successful. ProjectId: {ProjectId}", projectId);
    }

    public async Task DeleteTicketByUserIdAsync(Guid userId, IDbTransaction transaction)
    {
        _logger.LogWarning("Deleting Tickets by user called. UserId: {UserId}", userId);

        await DeleteTicketsAsync(
            tr => _ticketRepository.GetAllByReporterIdAsync(userId, tr),
            tr => _ticketRepository.DeleteAllByUserIdAsync(userId, tr),
            transaction);

        _logger.LogInformation("Deleting Tickets by user successful. UserId: {UserId}", userId);
    }

    public async Task DeleteTicketByPriorityIdAsync(Guid priorityId, IDbTransaction transaction)
    {
        _logger.LogInformation("Deleting Tickets by priority called. PriorityId: {PriorityId}", priorityId);

        await DeleteTicketsAsync(
            tr => _ticketRepository.GetAllByPriorityIdAsync(priorityId, tr),
            tr => _ticketRepository.DeleteAllByPriorityIdAsync(priorityId, tr),
            transaction);

        _logger.LogInformation("Deleting Tickets by priority successful. PriorityId: {PriorityId}", priorityId);
    }

    /// <summary>
    /// Deletes Tickets and related Comments and TicketRelations
    /// </summary>
    /// <param name="getTickets">Method that gets the list of tickets</param>
    /// <param name="deleteTickets">Method that deletes tickets by specific parameter</param>
    /// <param name="transaction"></param>
    /// <returns></returns>
    private async Task DeleteTicketsAsync(
        Func<IDbTransaction, Task<IEnumerable<Ticket>>> getTickets,
        Func<IDbTransaction, Task<bool>> deleteTickets,
        IDbTransaction transaction)
    {
        var tickets = await getTickets(transaction);

        foreach (var ticket in tickets)
        {
            await _ticketRelationService.DeleteTicketRelationByTicketIdAsync(ticket.Id, transaction);
            await _commentService.DeleteByTicketIdAsync(ticket.Id, transaction);
        }

        if (!await deleteTickets(transaction))
        {
            _logger.LogError("Deleting Tickets failed. ProjectId: {ProjectId}", tickets.FirstOrDefault()?.ProjectId);
            throw new OperationFailedException("Deleting Tickets failed.");
        }
    }

    /// <summary>
    /// Gets TicketDto with all related entities
    /// </summary>
    /// <param name="ticket"></param>
    /// <returns></returns>
    private async Task<TicketDto> GetTicketDtoWithRelationsAsync(Ticket ticket)
    {
        try
        {
            var ticketDto = _mapper.Map<TicketDto>(ticket);
            ticketDto.Comments = await _commentService.GetByTicketIdAsync(ticket.Id);
            ticketDto.Tags = await _tagService.GetTagsByTicketIdAsync(ticket.Id);
            ticketDto.RelationsAsSource = await _ticketRelationService.GetTicketRelationsBySourceIdAsync(ticket.Id);
            ticketDto.RelationsAsTarget = await _ticketRelationService.GetTicketRelationsByTargetIdAsync(ticket.Id);

            _logger.LogInformation("Getting Ticket relations successful. Ticket: {TicketId}", ticket.Id);
            return ticketDto;
        }
        catch (Exception ex)
        {
            _logger.LogError("Getting Ticket relations failed. Ticket: {TicketId}", ticket.Id);
            throw new OperationFailedException("Getting Ticket relations failed.");
        }
    }
}
