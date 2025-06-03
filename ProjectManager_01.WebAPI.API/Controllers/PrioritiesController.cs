using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using ProjectManager_01.Application.Contracts.Repositories;
using ProjectManager_01.Application.Contracts.Services;
using ProjectManager_01.Application.DTOs.Priorities;

namespace ProjectManager_01.Controllers;

[EnableRateLimiting("fixedlimit")]
[Route("api/[controller]")]
[ApiController]
public class PrioritiesController : ControllerBase
{
    private readonly IPriorityService priorityService;

    public PrioritiesController(IPriorityService priorityService)
    {
        this.priorityService = priorityService;
    }

    // GET api/priorities
    /// <summary>
    /// Get all priorities
    /// </summary>
    /// <returns>All priorities</returns>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<PriorityDto>>> GetPriorities()
    {
        try
        {
            return Ok(await priorityService.GetAllPrioritiesAsync());
        }
        catch (Exception ex)
        {
            return Problem(ex.Message);
        }
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
        try
        {
            return Ok(await priorityService.GetPriorityByIdAsync(id));
        }
        catch (Exception ex)
        {
            return Problem(ex.Message);
        }
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
        try
        {
            await priorityService.CreatePriorityAsync(priority);
            return Ok();
        }
        catch (Exception ex)
        {
            return Problem(ex.Message);
        }
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
        try
        {
            await priorityService.UpdatePriorityAsync(updatedPriority);
            return Ok();
        }
        catch (Exception ex)
        {
            return Problem(ex.Message);
        }
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
        try
        {
            await priorityService.DeletePriorityAsync(id);
            return Ok();
        }
        catch (Exception ex)
        {
            return Problem(ex.Message);
        }
    }
}