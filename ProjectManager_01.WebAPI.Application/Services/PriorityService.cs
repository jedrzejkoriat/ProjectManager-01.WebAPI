using System.Data;
using AutoMapper;
using Microsoft.Data.SqlClient;
using ProjectManager_01.Application.Contracts.Factories;
using ProjectManager_01.Application.Contracts.Repositories;
using ProjectManager_01.Application.Contracts.Services;
using ProjectManager_01.Application.DTOs.Priorities;
using ProjectManager_01.Domain.Models;

namespace ProjectManager_01.Application.Services;

public class PriorityService : IPriorityService
{

    private readonly IPriorityRepository priorityRepository;
    private readonly IMapper mapper;
    private readonly IDbConnectionFactory dbConnectionFactory;
    private readonly ITicketService ticketService;

    public PriorityService(
        IPriorityRepository priorityRepository, 
        IMapper mapper,
        IDbConnectionFactory dbConnectionFactory,
        ITicketService ticketService)
    {
        this.priorityRepository = priorityRepository;
        this.mapper = mapper;
        this.dbConnectionFactory = dbConnectionFactory;
        this.ticketService = ticketService;
    }

    public async Task CreatePriorityAsync(PriorityCreateDto priorityCreateDto)
    {
        Priority priority = mapper.Map<Priority>(priorityCreateDto);
        await priorityRepository.CreateAsync(priority);
    }

    public async Task DeletePriorityAsync(Guid priorityId)
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
            await ticketService.DeleteTicketByPriorityIdAsync(priorityId, connection, transaction);
            await priorityRepository.DeleteAsync(priorityId);

            transaction.Commit();
        }
        catch
        {
            transaction.Rollback();
            throw new Exception("Error while performing priority deletion transaction.");
        }

    }

    public async Task<List<PriorityDto>> GetAllPrioritiesAsync()
    {
        List<Priority> priorities = await priorityRepository.GetAllAsync();

        return mapper.Map<List<PriorityDto>>(priorities);
    }

    public async Task<PriorityDto> GetPriorityByIdAsync(Guid priorityId)
    {
        Priority priority = await priorityRepository.GetByIdAsync(priorityId);

        return mapper.Map<PriorityDto>(priority);
    }

    public async Task UpdatePriorityAsync(PriorityUpdateDto priorityUpdateDto)
    {
        Priority priority = mapper.Map<Priority>(priorityUpdateDto);
        await priorityRepository.UpdateAsync(priority);
    }
}
