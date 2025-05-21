using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using ProjectManager_01.WebAPI.Data;

namespace ProjectManager_01.WebAPI.Controllers;

[EnableRateLimiting("fixedlimit")]
[Route("api/[controller]")]
[ApiController]
public class TicketsController : ControllerBase
{
    private static List<Ticket> tickets = new List<Ticket>
        {
            new Ticket
            {
                Id = Guid.NewGuid(),
                ProjectId = Guid.NewGuid(),
                PriorityId = Guid.NewGuid(),
                AssigneeId = null,
                ReporterId = Guid.NewGuid(),
                Resolution = Enums.Resolution.Unresolved,
                Status = Enums.Status.Open,
                TicketType = Enums.TicketType.Bug,
                Title = "Sample Ticket",
                Description = "Description here",
                Version = "1.0",
                CreatedAt = DateTime.Now
            }
        };

    // GET: api/tickets
    /// <summary>
    /// Get all tickets
    /// </summary>
    /// <returns>All tickets</returns>
    [HttpGet]
    public ActionResult<IEnumerable<Ticket>> GetTickets()
    {
        return Ok(tickets);
    }

    // GET: api/tickets/{id}
    /// <summary>
    /// Get a ticket by ID
    /// </summary>
    /// <param name="id"></param>
    /// <returns>Ticket by its id</returns>
    [HttpGet("{id}")]
    public ActionResult<Ticket> GetTicket(Guid id)
    {
        var ticket = tickets.FirstOrDefault(t => t.Id == id);

        if (ticket == null) 
            return NotFound();

        return Ok(ticket);
    }

    // POST: api/tickets
    /// <summary>
    /// Create a new ticket
    /// </summary>
    /// <param name="ticket"></param>
    /// <returns></returns>
    [HttpPost]
    public ActionResult Post([FromBody] Ticket ticket)
    {
        ticket.Id = Guid.NewGuid();
        tickets.Add(ticket);

        return CreatedAtAction(nameof(GetTicket), new { id = ticket.Id }, ticket);
    }

    // PUT: api/tickets/{id}
    /// <summary>
    /// Update an existing ticket
    /// </summary>
    /// <param name="id"></param>
    /// <param name="updatedTicket"></param>
    /// <returns></returns>
    [HttpPut("{id}")]
    public ActionResult Put(Guid id, [FromBody] Ticket updatedTicket)
    {
        var ticket = tickets.FirstOrDefault(t => t.Id == id);

        if (ticket == null) 
            return NotFound();

        ticket.ProjectId = updatedTicket.ProjectId;
        ticket.TicketType = updatedTicket.TicketType;
        ticket.PriorityId = updatedTicket.PriorityId;
        ticket.Status = updatedTicket.Status;
        ticket.AssigneeId = updatedTicket.AssigneeId;
        ticket.ReporterId = updatedTicket.ReporterId;
        ticket.Resolution = updatedTicket.Resolution;
        ticket.Title = updatedTicket.Title;
        ticket.Description = updatedTicket.Description;
        ticket.Version = updatedTicket.Version;
        ticket.CreatedAt = updatedTicket.CreatedAt;

        return NoContent();
    }

    // DELETE: api/tickets/{id}
    /// <summary>
    /// Delete a ticket
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete("{id}")]
    public ActionResult Delete(Guid id)
    {
        var ticket = tickets.FirstOrDefault(t => t.Id == id);

        if (ticket == null) 
            return NotFound();

        tickets.Remove(ticket);

        return NoContent();
    }
}