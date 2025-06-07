using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using ProjectManager_01.Application.Constants;
using ProjectManager_01.Application.Contracts.Services;
using ProjectManager_01.Application.DTOs.Priorities;

namespace ProjectManager_01.Controllers;

/// <summary>
/// Controller for managing Priorities (readonly) - Admin or User authorization.
/// </summary>
[EnableRateLimiting("fixedlimit")]
[Route("api/[controller]")]
[ApiController]
[Authorize]
public class PrioritiesController : ControllerBase
{
    private readonly IPriorityService _priorityService;

    public PrioritiesController(IPriorityService priorityService)
    {
        _priorityService = priorityService;
    }

    // GET api/priorities
    /// <summary>
    /// Get all Priorities
    /// </summary>
    /// <returns>All Priorities</returns>
    [HttpGet("")]
    public async Task<ActionResult<IEnumerable<PriorityDto>>> GetPriorities()
    {
        return Ok(await _priorityService.GetAllPrioritiesAsync());
    }

    // GET api/priorities/{id}
    /// <summary>
    /// Get Priority by Id - Admin or User
    /// </summary>
    /// <param name="id"></param>
    /// <returns>Priority by Id</returns>
    [HttpGet("{id}")]
    public async Task<ActionResult<PriorityDto>> GetPriority(Guid id)
    {
        return Ok(await _priorityService.GetPriorityByIdAsync(id));
    }

    // POST api/priorities
    /// <summary>
    /// Create Priority - Admin only (INSERT is denied on db side)
    /// </summary>
    /// <param name="priority"></param>
    /// <returns></returns>
    [HttpPost]
    [Authorize(Roles = Roles.Admin)]
    public async Task<ActionResult> CreatePriority([FromBody] PriorityCreateDto priority)
    {
        await _priorityService.CreatePriorityAsync(priority);
        return Ok();
    }

    // PUT api/priorities
    /// <summary>
    /// Update Priority - Admin only (UPDATE is denied on db side)
    /// </summary>
    /// <param name="updatedPriority"></param>
    /// <returns></returns>
    [HttpPut]
    [Authorize(Roles = Roles.Admin)]
    public async Task<ActionResult> UpdatePriority([FromBody] PriorityUpdateDto updatedPriority)
    {
        await _priorityService.UpdatePriorityAsync(updatedPriority);
        return Ok();
    }

    // DELETE api/priorities/{id}
    /// <summary>
    /// Delete Priority by Id - Admin only (DELETE is denied on db side)
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete("{id}")]
    [Authorize(Roles = Roles.Admin)]
    public async Task<ActionResult> DeletePriority(Guid id)
    {
        await _priorityService.DeletePriorityAsync(id);
        return Ok();
    }
}