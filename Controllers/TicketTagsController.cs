using Microsoft.AspNetCore.Mvc;
using ProjectManager_01.WebAPI.Data;

namespace ProjectManager_01.WebAPI.Controllers;
[Route("api/[controller]")]
[ApiController]
public class TicketTagsController : ControllerBase
{
    private static List<TicketTag> ticketTags = new List<TicketTag>
    {
        new TicketTag { Id = Guid.NewGuid(), TicketId = Guid.NewGuid(), TagId = Guid.NewGuid() },
        new TicketTag { Id = Guid.NewGuid(), TicketId = Guid.NewGuid(), TagId = Guid.NewGuid() },
    };

    // GET: api/tickettags
    /// <summary>
    /// Get all ticket tags
    /// </summary>
    /// <returns>All ticket tags</returns>
    [HttpGet]
    public ActionResult<IEnumerable<Ticket>> GetTickets()
    {
        return Ok(ticketTags);
    }

    // GET: api/tickettags/{id}
    /// <summary>
    /// Get a ticket tag by ID
    /// </summary>
    /// <param name="id"></param>
    /// <returns>Ticket tag by its id</returns>
    [HttpGet("{id}")]
    public ActionResult<Ticket> GetTicket(Guid id)
    {
        var ticketTag = ticketTags.FirstOrDefault(t => t.Id == id);

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
    public ActionResult Post([FromBody] TicketTag ticketTag)
    {
        ticketTag.Id = Guid.NewGuid();
        ticketTags.Add(ticketTag);

        return CreatedAtAction(nameof(GetTicket), new { id = ticketTag.Id }, ticketTag);
    }

    // PUT: api/tickettags/{id}
    /// <summary>
    /// Update an existing ticket tag
    /// </summary>
    /// <param name="id"></param>
    /// <param name="updatedTicketTag"></param>
    /// <returns></returns>
    [HttpPut("{id}")]
    public ActionResult Put(Guid id, [FromBody] TicketTag updatedTicketTag)
    {
        var ticketTag = ticketTags.FirstOrDefault(t => t.Id == id);

        if (ticketTag == null) 
            return NotFound();

        ticketTag.Id = updatedTicketTag.Id;
        ticketTag.TicketId = updatedTicketTag.TicketId;
        ticketTag.TagId = updatedTicketTag.TagId;

        return NoContent();
    }

    // DELETE: api/tickettags/{id}
    /// <summary>
    /// Delete a ticket tag by id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete("{id}")]
    public ActionResult Delete(Guid id)
    {
        var ticketTag = ticketTags.FirstOrDefault(t => t.Id == id);

        if (ticketTag == null) 
            return NotFound();

        ticketTags.Remove(ticketTag);

        return NoContent();
    }
}
