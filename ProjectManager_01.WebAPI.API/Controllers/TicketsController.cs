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
    [HttpGet("api/[controller]")]
    [Authorize(Roles = Roles.Admin)]
    public async Task<ActionResult<IEnumerable<TicketOverviewDto>>> GetTickets()
    {
        return Ok(await _ticketService.GetAllTicketsAsync());
    }

    // GET: api/projects/{projectId}/tickets/{id}
    /// <summary>
    /// Get Ticket by Id - User with ReadTicket permission and matching Project access
    /// </summary>
    /// <param name="id"></param>
    /// <param name="projectId"></param>
    /// <returns>Ticket by Id</returns>
    [HttpGet("api/projects/{projectId}/[controller]/{id}")]
    [Authorize(Policy = Permissions.ReadTicket)]
    public async Task<ActionResult<TicketDto>> GetTicket(Guid id, Guid projectId)
    {
        return Ok(await _ticketService.GetTicketByIdAsync(id, projectId));
    }

    // GET: api/projects/{projectId}/tickets/getByKeyAndNumber?projectKey=ABC&ticketNumber=123
    /// <summary>
    /// Get Ticket by ProjectKey and TicketNumber - User with ReadTicket permission and matching Project access
    /// </summary>
    /// <param name="projectKey"></param>
    /// <param name="ticketNumber"></param>
    /// <param name="projectId"></param>
    /// <returns>Ticket by ProjectKey and TicketNumber</returns>
    [HttpGet("api/projects/{projectId}/[controller]/getByKeyAndNumber")]
    [Authorize(Policy = Permissions.ReadTicket)]
    public async Task<ActionResult<TicketDto>> GetTicketByKeyAndNumber([FromQuery] string projectKey, [FromQuery] int ticketNumber, Guid projectId)
    {
        return Ok(await _ticketService.GetTicketByKeyAndNumberAsync(projectKey, ticketNumber, projectId));
    }

    // GET: api/projects/{projectId}/tickets/project/{projectId}
    /// <summary>
    /// Get Tickets by ProjectId - User with ReadTicket permission and matching Project access
    /// </summary>
    /// <param name="projectId"></param>
    /// <returns>Tickets by ProjectId</returns>
    [HttpGet("api/projects/{projectId}/[controller]")]
    [Authorize(Policy = Permissions.ReadTicket)]
    public async Task<ActionResult<IEnumerable<TicketOverviewDto>>> GetTicketsByProjectId(Guid projectId)
    {
        var tickets = await _ticketService.GetTicketsByProjectIdAsync(projectId);
        return Ok(tickets);
    }

    // POST: api/projects/{projectId}/tickets
    /// <summary>
    /// Create Ticket - User with WriteTicket permission and matching Project access
    /// </summary>
    /// <param name="ticket"></param>
    /// <param name="projectId"></param>
    /// <returns></returns>
    [HttpPost("api/projects/{projectId}/[controller]")]
    [Authorize(Policy = Permissions.WriteTicket)]
    public async Task<ActionResult> CreateTicket([FromBody] TicketCreateDto ticket, Guid projectId)
    {
        await _ticketService.CreateTicketAsync(ticket, projectId);
        return Ok();
    }

    // PUT: api/projects/{projectId}/tickets
    /// <summary>
    /// Update Ticket - User with WriteTicket permission and matching Project access
    /// </summary>
    /// <param name="updatedTicket"></param>
    /// <param name="projectId"></param>
    /// <returns></returns>
    [HttpPut("api/projects/{projectId}/[controller]")]
    [Authorize(Policy = Permissions.WriteTicket)]
    public async Task<ActionResult> UpdateTicket([FromBody] TicketUpdateDto updatedTicket, Guid projectId)
    {
        var ticket = await _ticketService.UpdateTicketAsync(updatedTicket, projectId);

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

    // PATCH api/projects/{projectId}/tickets/{id}/soft-delete
    /// <summary>
    /// Soft-delete Ticket by Id - User with DeleteTicket permission and matching Project access
    /// </summary>
    /// <param name="id"></param>
    /// <param name="projectId"></param>
    /// <returns></returns>
    [HttpPatch("api/projects/{projectId}/[controller]/{id}/soft-delete")]
    [Authorize(Policy = Permissions.DeleteTicket)]
    public async Task<ActionResult> SoftDeleteTicket(Guid id, Guid projectId)
    {
        await _ticketService.SoftDeleteTicketAsync(id, projectId);
        return Ok();
    }
}