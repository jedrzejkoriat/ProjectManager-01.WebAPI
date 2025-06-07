using System.Data;
using AutoMapper;
using ProjectManager_01.Application.Contracts.Repositories;
using ProjectManager_01.Application.Contracts.Services;
using ProjectManager_01.Application.DTOs.Priorities;
using ProjectManager_01.Application.Helpers;
using ProjectManager_01.Domain.Models;

namespace ProjectManager_01.Application.Services;

public class PriorityService : IPriorityService
{
    private readonly IPriorityRepository _priorityRepository;
    private readonly IMapper _mapper;
    private readonly IDbConnection _dbConnection;
    private readonly ITicketService _ticketService;

    public PriorityService(
        IPriorityRepository priorityRepository,
        IMapper mapper,
        IDbConnection dbConnection,
        ITicketService ticketService)
    {
        _priorityRepository = priorityRepository;
        _mapper = mapper;
        _dbConnection = dbConnection;
        _ticketService = ticketService;
    }

    public async Task CreatePriorityAsync(PriorityCreateDto priorityCreateDto)
    {
        var priority = _mapper.Map<Priority>(priorityCreateDto);
        await _priorityRepository.CreateAsync(priority);
    }

    public async Task DeletePriorityAsync(Guid priorityId)
    {
        using var transaction = DbTransactionHelper.BeginTransaction(_dbConnection);

        try
        {
            await _ticketService.DeleteTicketByPriorityIdAsync(priorityId, transaction);
            await _priorityRepository.DeleteByIdAsync(priorityId, transaction);

            transaction.Commit();
        }
        catch
        {
            transaction.Rollback();
            throw new Exception("Error while performing priority deletion transaction.");
        }

    }

    public async Task<IEnumerable<PriorityDto>> GetAllPrioritiesAsync()
    {
        var priorities = await _priorityRepository.GetAllAsync();

        return _mapper.Map<IEnumerable<PriorityDto>>(priorities);
    }

    public async Task<PriorityDto> GetPriorityByIdAsync(Guid priorityId)
    {
        var priority = await _priorityRepository.GetByIdAsync(priorityId);

        return _mapper.Map<PriorityDto>(priority);
    }

    public async Task UpdatePriorityAsync(PriorityUpdateDto priorityUpdateDto)
    {
        var priority = _mapper.Map<Priority>(priorityUpdateDto);
        await _priorityRepository.UpdateAsync(priority);
    }
}
