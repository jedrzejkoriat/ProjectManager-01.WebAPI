using ProjectManager_01.Application.DTOs.Priorities;

namespace ProjectManager_01.Application.Contracts.Services;

public interface IPriorityService
{
    Task CreatePriorityAsync(PriorityCreateDto priorityCreateDto);
    Task UpdatePriorityAsync(PriorityUpdateDto priorityUpdateDto);
    Task DeletePriorityAsync(Guid priorityId);
    Task<PriorityDto> GetPriorityByIdAsync(Guid priorityId);
    Task<IEnumerable<PriorityDto>> GetAllPrioritiesAsync();
}
