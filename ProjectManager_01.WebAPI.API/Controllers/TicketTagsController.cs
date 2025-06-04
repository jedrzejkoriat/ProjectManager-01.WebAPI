using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using ProjectManager_01.Application.Contracts.Services;
using ProjectManager_01.Application.DTOs.TicketTags;

namespace ProjectManager_01.Controllers;

[EnableRateLimiting("fixedlimit")]
[Route("api/[controller]")]
[ApiController]
public class TicketTagsController : ControllerBase
{
    private readonly ITicketTagService _ticketTagService;

    public TicketTagsController(ITicketTagService ticketTagService)
    {
        _ticketTagService = ticketTagService;
    }

    // GET: api/tickettags
    /// <summary>
    /// Get all ticket tags
    /// </summary>
    /// <returns>All ticket tags</returns>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<TicketTagDto>>> GetTicketTags()
    {
        try
        {
            return Ok(await _ticketTagService.GetAllTicketTagsAsync());
        }
        catch (Exception ex)
        {
            return Problem(ex.Message);
        }
    }

    // GET: api/tickettags/{ticketId}/{tagId}
    /// <summary>
    /// Get a ticket tag by ticket id and tag id
    /// </summary>
    /// <param name="ticketId"></param>
    /// <param name="tagId"></param>
    /// <returns>Ticket tag by its ticket id and tag id</returns>
    [HttpGet("{ticketId}/{tagId}")]
    public async Task<ActionResult<TicketTagDto>> GetTicketTag(Guid ticketId, Guid tagId)
    {
        try
        {
            return Ok(await _ticketTagService.GetTicketTagByIdAsync(ticketId, tagId));
        }
        catch (Exception ex)
        {
            return Problem(ex.Message);
        }
    }

    // POST: api/tickettags
    /// <summary>
    /// Create a new ticket tag
    /// </summary>
    /// <param name="ticketTag"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<ActionResult> CreateTicketTag([FromBody] TicketTagCreateDto ticketTag)
    {
        try
        {
            await _ticketTagService.CreateTicketTagAsync(ticketTag);
            return Ok();
        }
        catch (Exception ex)
        {
            return Problem(ex.Message);
        }
    }

    // DELETE: api/tickettags/{ticketId}/{tagId}
    /// <summary>
    /// Delete a ticket tag by ticket id and tag id
    /// </summary>
    /// <param name="ticketId"></param>
    /// <param name="tagId"></param>
    /// <returns></returns>
    [HttpDelete("{ticketId}/{tagId}")]
    public async Task<ActionResult> DeleteTicketTag(Guid ticketId, Guid tagId)
    {
        try
        {
            await _ticketTagService.DeleteTicketTagAsync(ticketId, tagId);
            return Ok();
        }
        catch (Exception ex)
        {
            return Problem(ex.Message);
        }
    }
}
