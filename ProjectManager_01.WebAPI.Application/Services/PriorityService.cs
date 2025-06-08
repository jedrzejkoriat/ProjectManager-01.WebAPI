using System.Data;
using AutoMapper;
using Microsoft.Extensions.Logging;
using ProjectManager_01.Application.Contracts.Repositories;
using ProjectManager_01.Application.Contracts.Services;
using ProjectManager_01.Application.DTOs.Priorities;
using ProjectManager_01.Application.Exceptions;
using ProjectManager_01.Application.Helpers;
using ProjectManager_01.Domain.Models;

namespace ProjectManager_01.Application.Services;

public class PriorityService : IPriorityService
{
    private readonly IPriorityRepository _priorityRepository;
    private readonly IMapper _mapper;
    private readonly IDbConnection _dbConnection;
    private readonly ITicketService _ticketService;
    private readonly ILogger<PriorityService> _logger;

    public PriorityService(
        IPriorityRepository priorityRepository,
        IMapper mapper,
        IDbConnection dbConnection,
        ITicketService ticketService,
        ILogger<PriorityService> logger)
    {
        _priorityRepository = priorityRepository;
        _mapper = mapper;
        _dbConnection = dbConnection;
        _ticketService = ticketService;
        _logger = logger;
    }

    public async Task CreatePriorityAsync(PriorityCreateDto priorityCreateDto)
    {
        _logger.LogWarning("Creating Priority called. Priority: {PriorityName}", priorityCreateDto.Name);

        var priority = _mapper.Map<Priority>(priorityCreateDto);
        priority.Id = Guid.NewGuid();

        // Check if operation is successful
        if (!await _priorityRepository.CreateAsync(priority))
        {
            _logger.LogError("Creating Priority failed. Priority: {PriorityName}", priorityCreateDto.Name);
            throw new OperationFailedException("Creating priority failed.");
        }

        _logger.LogInformation("Creating priority successful. Priority: {PriorityIId}", priority.Id);
    }

    public async Task DeletePriorityAsync(Guid priorityId)
    {
        _logger.LogWarning("Deleting Priority transaction called. Priority: {PriorityId}", priorityId);

        using var transaction = DbTransactionHelper.BeginTransaction(_dbConnection);

        try
        {
            await _ticketService.DeleteTicketByPriorityIdAsync(priorityId, transaction);

            // Check if operation is successful
            if (!await _priorityRepository.DeleteByIdAsync(priorityId, transaction))
            {
                _logger.LogWarning("Deleting Priority failed. Priority: {PriorityId}", priorityId);
                throw new OperationFailedException("Deleting Priority transaction failed.");
            }

            transaction.Commit();
            _logger.LogInformation("Deleting Priority transaction successful. Priority: {PriorityId}", priorityId);
        }
        catch
        {
            transaction.Rollback();
            _logger.LogInformation("Deleting Priority transaction failed. Priority: {PriorityId}", priorityId);
        }

    }

    public async Task<IEnumerable<PriorityDto>> GetAllPrioritiesAsync()
    {
        _logger.LogInformation("Getting Priorities called.");

        var priorities = await _priorityRepository.GetAllAsync();

        _logger.LogInformation("Getting Priorities ({Count}) successful.", priorities.Count());
        return _mapper.Map<IEnumerable<PriorityDto>>(priorities);
    }

    public async Task<PriorityDto> GetPriorityAsync(Guid priorityId)
    {
        _logger.LogInformation("Getting Priority called. Priority: {PriorityId}", priorityId);

        var priority = await _priorityRepository.GetByIdAsync(priorityId);

        // Check if operation is successful
        if (priority == null)
        {
            _logger.LogError("Getting Priority failed. Priority: {PriorityId}", priorityId);
            throw new NotFoundException("Priority not found.");
        }

        _logger.LogInformation("Getting Priority successful. Priority: {PriorityId}", priorityId);
        return _mapper.Map<PriorityDto>(priority);
    }

    public async Task UpdatePriorityAsync(PriorityUpdateDto priorityUpdateDto)
    {
        _logger.LogInformation("Updating Priority called. Priority: {PriorityId}", priorityUpdateDto.Id);

        var priority = _mapper.Map<Priority>(priorityUpdateDto);

        // Check if operation is successful
        if (!await _priorityRepository.UpdateAsync(priority))
        {
            _logger.LogError("Updating Priority failed. Priority: {PriorityId}", priorityUpdateDto.Id);
            throw new OperationFailedException("Updating Priority failed.");
        }

        _logger.LogInformation("Updating Priority successful. Priority: {PriorityId}", priorityUpdateDto.Id);
    }
}
