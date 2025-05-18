using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectManager_01.WebAPI.Data;

namespace ProjectManager_01.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TicketTypesController : ControllerBase
{
    private static List<TicketType> ticketTypes = new List<TicketType>
        {
            new TicketType { Id = 1, Name = "Bug" },
            new TicketType { Id = 2, Name = "Task" }
        };

    // GET: api/tickettypes
    /// <summary>
    /// Get all ticket types
    /// </summary>
    /// <returns>Get all ticket types</returns>
    [HttpGet]
    public ActionResult<IEnumerable<TicketType>> GetTicketTypes()
    {
        return Ok(ticketTypes);
    }

    // GET: api/tickettypes/{id}
    /// <summary>
    /// Get a ticket type by ID
    /// </summary>
    /// <param name="id"></param>
    /// <returns>Ticket type by its id</returns>
    [HttpGet("{id}")]
    public ActionResult<TicketType> GetTicketType(int id)
    {
        var ticketType = ticketTypes.FirstOrDefault(t => t.Id == id);
        if (ticketType == null) return NotFound();
        return Ok(ticketType);
    }

    // POST: api/tickettypes
    /// <summary>
    /// Create a new ticket type
    /// </summary>
    /// <param name="ticketType"></param>
    /// <returns></returns>
    [HttpPost]
    public ActionResult Post([FromBody] TicketType ticketType)
    {
        ticketType.Id = ticketTypes.Max(t => t.Id) + 1;
        ticketTypes.Add(ticketType);
        return CreatedAtAction(nameof(GetTicketType), new { id = ticketType.Id }, ticketType);
    }

    // PUT: api/tickettypes/{id}
    /// <summary>
    /// Update an existing ticket type
    /// </summary>
    /// <param name="id"></param>
    /// <param name="updatedTicketType"></param>
    /// <returns></returns>
    [HttpPut("{id}")]
    public ActionResult Put(int id, [FromBody] TicketType updatedTicketType)
    {
        var ticketType = ticketTypes.FirstOrDefault(t => t.Id == id);
        if (ticketType == null) return NotFound();

        ticketType.Name = updatedTicketType.Name;
        return NoContent();
    }

    // DELETE: api/tickettypes/{id}
    /// <summary>
    /// Delete a ticket type
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete("{id}")]
    public ActionResult Delete(int id)
    {
        var ticketType = ticketTypes.FirstOrDefault(t => t.Id == id);
        if (ticketType == null) return NotFound();

        ticketTypes.Remove(ticketType);
        return NoContent();
    }
}