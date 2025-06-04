using System.Data;
using AutoMapper;
using ProjectManager_01.Application.Contracts.Repositories;
using ProjectManager_01.Application.Contracts.Services;
using ProjectManager_01.Application.DTOs.Priorities;
using ProjectManager_01.Domain.Models;

namespace ProjectManager_01.Application.Services;

public class PriorityService : IPriorityService
{
    private readonly IPriorityRepository priorityRepository;
    private readonly IMapper mapper;
    private readonly IDbConnection dbConnection;
    private readonly ITicketService ticketService;

    public PriorityService(
        IPriorityRepository priorityRepository,
        IMapper mapper,
        IDbConnection dbConnection,
        ITicketService ticketService)
    {
        this.priorityRepository = priorityRepository;
        this.mapper = mapper;
        this.dbConnection = dbConnection;
        this.ticketService = ticketService;
    }

    public async Task CreatePriorityAsync(PriorityCreateDto priorityCreateDto)
    {
        var priority = mapper.Map<Priority>(priorityCreateDto);
        await priorityRepository.CreateAsync(priority);
    }

    public async Task DeletePriorityAsync(Guid priorityId)
    {
        if (dbConnection.State != ConnectionState.Open)
            dbConnection.Open();

        using var transaction = dbConnection.BeginTransaction();

        try
        {
            await ticketService.DeleteTicketByPriorityIdAsync(priorityId, transaction);
            await priorityRepository.DeleteAsync(priorityId, transaction);

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
        var priorities = await priorityRepository.GetAllAsync();

        return mapper.Map<IEnumerable<PriorityDto>>(priorities);
    }

    public async Task<PriorityDto> GetPriorityByIdAsync(Guid priorityId)
    {
        var priority = await priorityRepository.GetByIdAsync(priorityId);

        return mapper.Map<PriorityDto>(priority);
    }

    public async Task UpdatePriorityAsync(PriorityUpdateDto priorityUpdateDto)
    {
        var priority = mapper.Map<Priority>(priorityUpdateDto);
        await priorityRepository.UpdateAsync(priority);
    }
}
