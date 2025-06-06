using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using ProjectManager_01.Application.Contracts.Services;
using ProjectManager_01.Application.DTOs.Priorities;

namespace ProjectManager_01.Controllers;

[EnableRateLimiting("fixedlimit")]
[Route("api/[controller]")]
[ApiController]
public class PrioritiesController : ControllerBase
{
    private readonly IPriorityService _priorityService;

    public PrioritiesController(IPriorityService priorityService)
    {
        _priorityService = priorityService;
    }

    // GET api/priorities
    /// <summary>
    /// Get all priorities
    /// </summary>
    /// <returns>All priorities</returns>
    [HttpGet("")]
    public async Task<ActionResult<IEnumerable<PriorityDto>>> GetPriorities()
    {
        return Ok(await _priorityService.GetAllPrioritiesAsync());
    }

    // GET api/priorities/{id}
    /// <summary>
    /// Get a priority by ID
    /// </summary>
    /// <param name="id"></param>
    /// <returns>Priority by id</returns>
    [HttpGet("{id}")]
    public async Task<ActionResult<PriorityDto>> GetPriority(Guid id)
    {
        return Ok(await _priorityService.GetPriorityByIdAsync(id));
    }

    // POST api/priorities
    /// <summary>
    /// Create a new priority
    /// </summary>
    /// <param name="priority"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<ActionResult> CreatePriority([FromBody] PriorityCreateDto priority)
    {
        await _priorityService.CreatePriorityAsync(priority);
        return Ok();
    }

    // PUT api/priorities
    /// <summary>
    /// Update an existing priority
    /// </summary>
    /// <param name="updatedPriority"></param>
    /// <returns></returns>
    [HttpPut]
    public async Task<ActionResult> UpdatePriority([FromBody] PriorityUpdateDto updatedPriority)
    {
        await _priorityService.UpdatePriorityAsync(updatedPriority);
        return Ok();
    }

    // DELETE api/priorities/{id}
    /// <summary>
    /// Delete a priority
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeletePriority(Guid id)
    {
        await _priorityService.DeletePriorityAsync(id);
        return Ok();
    }
}