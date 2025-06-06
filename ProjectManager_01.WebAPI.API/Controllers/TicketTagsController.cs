using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using ProjectManager_01.Application.Constants;
using ProjectManager_01.Application.Contracts.Services;
using ProjectManager_01.Application.DTOs.TicketTags;

namespace ProjectManager_01.Controllers;

/// <summary>
/// Controller for managing TicketTags - Admin or User authorization.
/// </summary>
[EnableRateLimiting("fixedlimit")]
[Route("api/[controller]")]
[ApiController]
[Authorize]
public class TicketTagsController : ControllerBase
{
    private readonly ITicketTagService _ticketTagService;

    public TicketTagsController(ITicketTagService ticketTagService)
    {
        _ticketTagService = ticketTagService;
    }

    // GET: api/tickettags
    /// <summary>
    /// Get all TicketTags - Admin only
    /// </summary>
    /// <returns>All TicketTags</returns>
    [HttpGet]
    [Authorize(Roles = Roles.Admin)]
    public async Task<ActionResult<IEnumerable<TicketTagDto>>> GetTicketTags()
    {
        return Ok(await _ticketTagService.GetAllTicketTagsAsync());
    }

    // GET: api/tickettags/{ticketId}/{tagId}
    /// <summary>
    /// Get TicketTag by TicketId and TagId - User with ReadTicketTag permission and matching Project access
    /// </summary>
    /// <param name="ticketId"></param>
    /// <param name="tagId"></param>
    /// <returns>TicketTag by TicketId and TagId</returns>
    [HttpGet("{ticketId}/{tagId}")]
    [Authorize(Policy = Permissions.ReadTicketTag)]
    public async Task<ActionResult<TicketTagDto>> GetTicketTag(Guid ticketId, Guid tagId)
    {
        return Ok(await _ticketTagService.GetTicketTagByIdAsync(ticketId, tagId));
    }

    // POST: api/tickettags
    /// <summary>
    /// Create TicketTag - User with WriteTicketTag permission and matching Project access
    /// </summary>
    /// <param name="ticketTag"></param>
    /// <returns></returns>
    [HttpPost]
    [Authorize(Policy = Permissions.WriteTicketTag)]
    public async Task<ActionResult> CreateTicketTag([FromBody] TicketTagCreateDto ticketTag)
    {
        await _ticketTagService.CreateTicketTagAsync(ticketTag);
        return Ok();
    }

    // DELETE: api/tickettags/{ticketId}/{tagId}
    /// <summary>
    /// Delete TicketTag by TicketId and TagId - User with WriteTicketTag permission and matching Project access
    /// </summary>
    /// <param name="ticketId"></param>
    /// <param name="tagId"></param>
    /// <returns></returns>
    [HttpDelete("{ticketId}/{tagId}")]
    [Authorize(Policy = Permissions.WriteTicketTag)]
    public async Task<ActionResult> DeleteTicketTag(Guid ticketId, Guid tagId)
    {
        await _ticketTagService.DeleteTicketTagAsync(ticketId, tagId);
        return Ok();
    }
}
