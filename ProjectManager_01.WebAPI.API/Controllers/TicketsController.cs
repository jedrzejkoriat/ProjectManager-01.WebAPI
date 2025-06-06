using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.AspNetCore.SignalR;
using ProjectManager_01.Application.Constants;
using ProjectManager_01.Application.Contracts.Services;
using ProjectManager_01.Application.DTOs.Tickets;
using ProjectManager_01.Hubs;

namespace ProjectManager_01.Controllers;

/// <summary>
/// Controller for managing Tickets - Admin or User authorization.
/// </summary>
[EnableRateLimiting("fixedlimit")]
[Route("api/[controller]")]
[ApiController]
[Authorize]
public class TicketsController : ControllerBase
{
    private readonly ITicketService _ticketService;
    private readonly IHubContext<TicketsHub> _hubContext;

    public TicketsController(
        ITicketService ticketService,
        IHubContext<TicketsHub> hubContext)
    {
        _ticketService = ticketService;
        _hubContext = hubContext;
    }

    // GET: api/tickets
    /// <summary>
    /// Get all Tickets - Admin only
    /// </summary>
    /// <returns>All Tickets</returns>
    [HttpGet]
    [Authorize(Roles = Roles.Admin)]
    public async Task<ActionResult<IEnumerable<TicketDto>>> GetTickets()
    {
        return Ok(await _ticketService.GetAllTicketsAsync());
    }

    // GET: api/tickets/{id}
    /// <summary>
    /// Get Ticket by Id - User with ReadTicket permission and matching Project access
    /// </summary>
    /// <param name="id"></param>
    /// <returns>Ticket by Id</returns>
    [HttpGet("{id}")]
    [Authorize(Policy = Permissions.ReadTicket)]
    public async Task<ActionResult<TicketDto>> GetTicket(Guid id)
    {
        return Ok(await _ticketService.GetTicketByIdAsync(id));
    }

    // GET: api/tickets/getByKeyAndNumber?projectKey=ABC&ticketNumber=123
    /// <summary>
    /// Get Ticket by ProjectKey and TicketNumber - User with ReadTicket permission and matching Project access
    /// </summary>
    /// <param name="projectKey"></param>
    /// <param name="ticketNumber"></param>
    /// <returns>Ticket by ProjectKey and TicketNumber</returns>
    [HttpGet("getByKeyAndNumber")]
    [Authorize(Policy = Permissions.ReadTicket)]
    public async Task<ActionResult<TicketDto>> GetTicketByKeyAndNumber([FromQuery] string projectKey, [FromQuery] int ticketNumber)
    {
        return Ok(await _ticketService.GetTicketByKeyAndNumberAsync(projectKey, ticketNumber));
    }

    // GET: api/tickets/project/{projectId}
    /// <summary>
    /// Get Tickets by ProjectId - User with ReadTicket permission and matching Project access
    /// </summary>
    /// <param name="projectId"></param>
    /// <returns>Tickets by ProjectId</returns>
    [HttpGet("project/{projectId}")]
    [Authorize(Policy = Permissions.ReadTicket)]
    public async Task<ActionResult<IEnumerable<TicketDto>>> GetTicketsByProjectId(Guid projectId)
    {
        var tickets = await _ticketService.GetTicketsByProjectIdAsync(projectId);
        return Ok(tickets);
    }

    // POST: api/tickets
    /// <summary>
    /// Create Ticket - User with WriteTicket permission and matching Project access
    /// </summary>
    /// <param name="ticket"></param>
    /// <returns></returns>
    [HttpPost]
    [Authorize(Policy = Permissions.WriteTicket)]
    public async Task<ActionResult> CreateTicket([FromBody] TicketCreateDto ticket)
    {
        await _ticketService.CreateTicketAsync(ticket);
        return Ok();
    }

    // PUT: api/tickets
    /// <summary>
    /// Update Ticket - User with WriteTicket permission and matching Project access
    /// </summary>
    /// <param name="updatedTicket"></param>
    /// <returns></returns>
    [HttpPut]
    [Authorize(Policy = Permissions.WriteTicket)]
    public async Task<ActionResult> UpdateTicket([FromBody] TicketUpdateDto updatedTicket)
    {
        var ticket = await _ticketService.UpdateTicketAsync(updatedTicket);

        await _hubContext.Clients
            .Group($"ticket-{ticket.Id}")
            .SendAsync("ReceiveTicket", ticket);

        return Ok();
    }

    // DELETE: api/tickets/{id}
    /// <summary>
    /// Delete Ticket by Id - Admin only (DELETE is denied on db side)
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete("{id}")]
    [Authorize(Roles = Roles.Admin)]
    public async Task<ActionResult> DeleteTicket(Guid id)
    {
        await _ticketService.DeleteTicketAsync(id);
        return Ok();
    }

    // PATCH api/tickets/{id}/soft-delete
    /// <summary>
    /// Soft-delete Ticket by Id - User with DeleteTicket permission and matching Project access
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpPatch("{id}/soft-delete")]
    [Authorize(Policy = Permissions.DeleteTicket)]
    public async Task<ActionResult> SoftDeleteTicket(Guid id)
    {
        await _ticketService.SoftDeleteTicketAsync(id);
        return Ok();
    }
}