using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using ProjectManager_01.Application.Contracts.Services;
using ProjectManager_01.Application.DTOs;
using ProjectManager_01.Application.DTOs.Tickets;

namespace ProjectManager_01.Controllers;

[EnableRateLimiting("fixedlimit")]
[Route("api/[controller]")]
[ApiController]
public class TicketsController : ControllerBase
{
    private readonly ITicketService ticketService;

    public TicketsController(ITicketService ticketService)
    {
        this.ticketService = ticketService;
    }

    // GET: api/tickets
    /// <summary>
    /// Get all tickets
    /// </summary>
    /// <returns>All tickets</returns>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<TicketDto>>> GetTickets()
    {
        try
        {
            return Ok(await ticketService.GetAllTicketsAsync());
        }
        catch (Exception ex)
        {
            return Problem(ex.Message);
        }
    }

    // GET: api/tickets/{id}
    /// <summary>
    /// Get a ticket by ID
    /// </summary>
    /// <param name="id"></param>
    /// <returns>Ticket by its id</returns>
    [HttpGet("{id}")]
    public async Task<ActionResult<TicketDto>> GetTicket(Guid id)
    {
        try
        {
            return Ok(await ticketService.GetTicketByIdAsync(id));
        }
        catch (Exception ex)
        {
            return Problem(ex.Message);
        }
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
        try
        {
            await ticketService.CreateTicketAsync(ticket);
            return Ok();
        }
        catch (Exception ex)
        {
            return Problem(ex.Message);
        }
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
        try
        {
            await ticketService.UpdateTicketAsync(updatedTicket);
            return Ok();
        }
        catch (Exception ex)
        {
            return Problem(ex.Message);
        }
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
        try
        {
            await ticketService.DeleteTicketAsync(id);
            return Ok();
        }
        catch (Exception ex)
        {
            return Problem(ex.Message);
        }
    }

    // PATCH api/tickets/{id}/soft-delete
    /// <summary>
    /// Soft delete a ticket
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpPatch("{id}")]
    public async Task<ActionResult> SoftDeleteTicket(Guid id)
    {
        try
        {
            // await ticketService.SoftDeleteTicketAsync(id);
            return Ok();
        }
        catch (Exception ex)
        {
            return Problem(ex.Message);
        }
    }
}