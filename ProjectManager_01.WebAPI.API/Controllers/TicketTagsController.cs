using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using ProjectManager_01.API.DTOs;

namespace ProjectManager_01.Controllers;

[EnableRateLimiting("fixedlimit")]
[Route("api/[controller]")]
[ApiController]
public class TicketTagsController : ControllerBase
{
    private static List<TicketTagDTO> ticketTags = new List<TicketTagDTO>
    {
        new TicketTagDTO { TicketId = Guid.NewGuid(), TagId = Guid.NewGuid() },
        new TicketTagDTO { TicketId = Guid.NewGuid(), TagId = Guid.NewGuid() },
    };

    // GET: api/tickettags
    /// <summary>
    /// Get all ticket tags
    /// </summary>
    /// <returns>All ticket tags</returns>
    [HttpGet]
    public ActionResult<IEnumerable<TicketTagDTO>> GetTicketTags()
    {
        return Ok(ticketTags);
    }

    // GET: api/tickettags/{ticketId}/{tagId}
    /// <summary>
    /// Get a ticket tag by ticket id and tag id
    /// </summary>
    /// <param name="ticketId"></param>
    /// <param name="tagId"></param>
    /// <returns>Ticket tag by its ticket id and tag id</returns>
    [HttpGet("{ticketId}/{tagId}")]
    public ActionResult<TicketTagDTO> GetTicketTag(Guid ticketId, Guid tagId)
    {
        var ticketTag = ticketTags.FirstOrDefault(t => t.TicketId == ticketId && t.TagId == tagId);

        if (ticketTag == null)
            return NotFound();

        return Ok(ticketTag);
    }

    // POST: api/tickettags
    /// <summary>
    /// Create a new ticket tag
    /// </summary>
    /// <param name="ticketTag"></param>
    /// <returns></returns>
    [HttpPost]
    public ActionResult Post([FromBody] TicketTagDTO ticketTag)
    {
        ticketTags.Add(ticketTag);

        return CreatedAtAction(nameof(GetTicketTag), new { ticketId = ticketTag.TicketId, tagId = ticketTag.TagId }, ticketTag);
    }

    // PUT: api/tickettags/{ticketId}/{tagId}
    /// <summary>
    /// Update an existing ticket tag
    /// </summary>
    /// <param name="ticketId"></param>
    /// <param name="tagId"></param>
    /// <param name="updatedTicketTag"></param>
    /// <returns></returns>
    [HttpPut("{ticketId}/{tagId}")]
    public ActionResult Put(Guid ticketId, Guid tagId, [FromBody] TicketTagDTO updatedTicketTag)
    {
        var ticketTag = ticketTags.FirstOrDefault(t => t.TicketId == ticketId && t.TagId == tagId);

        if (ticketTag == null)
            return NotFound();

        ticketTag.TicketId = updatedTicketTag.TicketId;
        ticketTag.TagId = updatedTicketTag.TagId;

        return NoContent();
    }

    // DELETE: api/tickettags/{ticketId}/{tagId}
    /// <summary>
    /// Delete a ticket tag by ticket id and tag id
    /// </summary>
    /// <param name="ticketId"></param>
    /// <param name="tagId"></param>
    /// <returns></returns>
    [HttpDelete("{ticketId}/{tagId}")]
    public ActionResult Delete(Guid ticketId, Guid tagId)
    {
        var ticketTag = ticketTags.FirstOrDefault(t => t.TicketId == ticketId && t.TagId == tagId);

        if (ticketTag == null)
            return NotFound();

        ticketTags.Remove(ticketTag);

        return NoContent();
    }
}
