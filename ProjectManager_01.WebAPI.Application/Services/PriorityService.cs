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

    public PriorityService(IPriorityRepository priorityRepository, IMapper mapper)
    {
        this.priorityRepository = priorityRepository;
        this.mapper = mapper;
    }

    public async Task CreatePriorityAsync(PriorityCreateDto priorityCreateDto)
    {
        Priority priority = mapper.Map<Priority>(priorityCreateDto);
        await priorityRepository.CreateAsync(priority);
    }

    public async Task DeletePriorityAsync(Guid priorityId)
    {
        await priorityRepository.DeleteAsync(priorityId);
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
