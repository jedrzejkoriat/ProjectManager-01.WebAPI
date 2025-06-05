using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using ProjectManager_01.Application.Contracts.Services;
using ProjectManager_01.Application.DTOs.Tickets;

namespace ProjectManager_01.Controllers;

[EnableRateLimiting("fixedlimit")]
[Route("api/[controller]")]
[ApiController]
public class TicketsController : ControllerBase
{
    private readonly ITicketService _ticketService;

    public TicketsController(ITicketService ticketService)
    {
        _ticketService = ticketService;
    }

    // GET: api/tickets
    /// <summary>
    /// Get all tickets
    /// </summary>
    /// <returns>All tickets</returns>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<TicketDto>>> GetTickets()
    {
        return Ok(await _ticketService.GetAllTicketsAsync());
    }

    // GET: api/tickets/{id}
    /// <summary>
    /// Get a ticket by ID
    /// </summary>
    /// <param name="id"></param>
    /// <returns>Ticket by its id</returns>
    [HttpGet("id/{id}")]
    public async Task<ActionResult<TicketDto>> GetTicket(Guid id)
    {
        return Ok(await _ticketService.GetTicketByIdAsync(id));
    }

    // GET: api/tickets/{projectKey}-{ticketNumber}
    /// <summary>
    /// Get a ticket by project key and ticket number
    /// </summary>
    /// <param name="projectKey"></param>
    /// <param name="ticketnumber"></param>
    /// <returns></returns>
    [HttpGet("{projectKey}-{ticketNumber}")]
    public async Task<ActionResult<TicketDto>> GetTicketByKeyAndNumber(string projectKey, int ticketnumber)
    {
        return Ok(await _ticketService.GetTicketByKeyAndNumberAsync(projectKey, ticketnumber));
    }

    // GET: api/tickets/project/{projectId}
    /// <summary>
    /// Get all tickets by project ID
    /// </summary>
    /// <param name="projectId"></param>
    /// <returns></returns>
    [HttpGet("project/{projectId}")]
    public async Task<ActionResult<IEnumerable<TicketDto>>> GetTicketsByProjectId(Guid projectId)
    {
        var tickets = await _ticketService.GetTicketsByProjectIdAsync(projectId);
        return Ok(tickets);
    }

    // POST: api/tickets
    /// <summary>
    /// Create a new ticket
    /// </summary>
    /// <param name="ticket"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<ActionResult> CreateTicket([FromBody] TicketCreateDto ticket)
    {
        await _ticketService.CreateTicketAsync(ticket);
        return Ok();
    }

    // PUT: api/tickets
    /// <summary>
    /// Update an existing ticket
    /// </summary>
    /// <param name="updatedTicket"></param>
    /// <returns></returns>
    [HttpPut]
    public async Task<ActionResult> UpdateTicket([FromBody] TicketUpdateDto updatedTicket)
    {
        await _ticketService.UpdateTicketAsync(updatedTicket);
        return Ok();
    }

    // DELETE: api/tickets/{id}
    /// <summary>
    /// Delete a ticket
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteTicket(Guid id)
    {
        await _ticketService.DeleteTicketAsync(id);
        return Ok();
    }

    // PATCH api/tickets/{id}/soft-delete
    /// <summary>
    /// Soft delete a ticket
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpPatch("{id}/soft-delete")]
    public async Task<ActionResult> SoftDeleteTicket(Guid id)
    {
        await _ticketService.SoftDeleteTicketAsync(id);
        return Ok();
    }
}