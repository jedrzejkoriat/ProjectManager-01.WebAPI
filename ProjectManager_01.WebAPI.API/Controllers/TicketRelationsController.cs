using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using ProjectManager_01.Application.Contracts.Services;
using ProjectManager_01.Application.DTOs.TicketRelations;

namespace ProjectManager_01.Controllers;

[EnableRateLimiting("fixedlimit")]
[Route("api/[controller]")]
[ApiController]
public class TicketRelationsController : ControllerBase
{
    private readonly ITicketRelationService _ticketRelationService;

    public TicketRelationsController(ITicketRelationService ticketRelationService)
    {
        _ticketRelationService = ticketRelationService;
    }

    // GET: api/ticketrelations
    /// <summary>
    /// Get all ticket relations
    /// </summary>
    /// <returns>All ticket relations</returns>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<TicketRelationDto>>> GetTicketRelations()
    {
        return Ok(await _ticketRelationService.GetAllTicketRelationsAsync());
    }

    // GET api/ticketrelations/{Id}
    /// <summary>
    /// Get a ticket relation by Id
    /// </summary>
    /// <param name="Id"></param>
    /// <returns>Ticket relation by id</returns>
    [HttpGet("{Id}")]
    public async Task<ActionResult<TicketRelationDto>> GetTicketRelation(Guid Id)
    {
        return Ok(await _ticketRelationService.GetTicketRelationByIdAsync(Id));
    }

    // POST api/ticketrelations
    /// <summary>
    /// Create a new ticket relation
    /// </summary>
    /// <param name="ticketRelation"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<ActionResult> CreateTicketRelation([FromBody] TicketRelationCreateDto ticketRelation)
    {
        await _ticketRelationService.CreateTicketRelationAsync(ticketRelation);
        return Ok();
    }

    // PUT api/ticketrelations
    /// <summary>
    /// Update an existing ticket relation
    /// </summary>
    /// <param name="updatedTicketRelation"></param>
    /// <returns></returns>
    [HttpPut]
    public async Task<ActionResult> UpdateTicketRelation([FromBody] TicketRelationUpdateDto updatedTicketRelation)
    {
        await _ticketRelationService.UpdateTicketRelationAsync(updatedTicketRelation);
        return Ok();
    }

    // DELETE api/ticketrelations/{Id}
    /// <summary>
    /// Delete a ticket relation
    /// </summary>
    /// <param name="Id"></param>
    /// <returns></returns>
    [HttpDelete("{Id}")]
    public async Task<ActionResult> DeleteTicketRelation(Guid Id)
    {
        await _ticketRelationService.DeleteTicketRelationAsync(Id);
        return Ok();
    }
}