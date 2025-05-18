using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectManager_01.WebAPI.Data;

namespace ProjectManager_01.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TicketsController : ControllerBase
{
    private static List<Ticket> tickets = new List<Ticket>
        {
            new Ticket
            {
                Id = 1,
                ProjectId = 1,
                TypeId = 1,
                PriorityId = 1,
                AssigneeId = null,
                ReporterId = 1,
                ResolutionId = 1,
                Status = Enums.Status.Open,
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
    public ActionResult<Ticket> GetTicket(int id)
    {
        var ticket = tickets.FirstOrDefault(t => t.Id == id);
        if (ticket == null) return NotFound();
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
        ticket.Id = tickets.Max(t => t.Id) + 1;
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
    public ActionResult Put(int id, [FromBody] Ticket updatedTicket)
    {
        var ticket = tickets.FirstOrDefault(t => t.Id == id);
        if (ticket == null) return NotFound();

        ticket.ProjectId = updatedTicket.ProjectId;
        ticket.TypeId = updatedTicket.TypeId;
        ticket.PriorityId = updatedTicket.PriorityId;
        ticket.Status = updatedTicket.Status;
        ticket.AssigneeId = updatedTicket.AssigneeId;
        ticket.ReporterId = updatedTicket.ReporterId;
        ticket.ResolutionId = updatedTicket.ResolutionId;
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
    public ActionResult Delete(int id)
    {
        var ticket = tickets.FirstOrDefault(t => t.Id == id);
        if (ticket == null) return NotFound();

        tickets.Remove(ticket);
        return NoContent();
    }
}