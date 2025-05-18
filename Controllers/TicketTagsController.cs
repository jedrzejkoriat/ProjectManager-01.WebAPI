using Microsoft.AspNetCore.Mvc;
using ProjectManager_01.WebAPI.Data;

namespace ProjectManager_01.WebAPI.Controllers;
[Route("api/[controller]")]
[ApiController]
public class TicketTagsController : ControllerBase
{
    private static List<TicketTag> ticketTags = new List<TicketTag>
    {
        new TicketTag { Id = 1, TicketId = 1, TagId = 1 },
        new TicketTag { Id = 2, TicketId = 1, TagId = 2 },
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
    public ActionResult<Ticket> GetTicket(int id)
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
        ticketTag.Id = ticketTags.Max(t => t.Id) + 1;
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
    public ActionResult Put(int id, [FromBody] TicketTag updatedTicketTag)
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
    public ActionResult Delete(int id)
    {
        var ticketTag = ticketTags.FirstOrDefault(t => t.Id == id);

        if (ticketTag == null) 
            return NotFound();

        ticketTags.Remove(ticketTag);

        return NoContent();
    }
}
