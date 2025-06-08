using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using ProjectManager_01.Application.Constants;
using ProjectManager_01.Application.Contracts.Services;
using ProjectManager_01.Application.DTOs.TicketRelations;

namespace ProjectManager_01.Controllers;

/// <summary>
/// Controller for managing Ticket Relations - Admin or User authorization.
/// </summary>
[EnableRateLimiting("fixedlimit")]
[ApiController]
[Authorize]
public class TicketRelationsController : ControllerBase
{
    private readonly ITicketRelationService _ticketRelationService;

    public TicketRelationsController(ITicketRelationService ticketRelationService)
    {
        _ticketRelationService = ticketRelationService;
    }

    // GET: api/ticketrelations
    /// <summary>
    /// Get all TicketRelations - Admin only
    /// </summary>
    /// <returns>All TicketRelations</returns>
    [HttpGet("api/[controller]")]
    [Authorize(Roles = Roles.Admin)]
    public async Task<ActionResult<IEnumerable<TicketRelationDto>>> GetTicketRelations()
    {
        return Ok(await _ticketRelationService.GetAllTicketRelationsAsync());
    }

    // GET api/projects/{projectId}/ticketrelations/{Id}
    /// <summary>
    /// Get TicketRelation by Id - User with ReadTicketRelation permission and matching Project access
    /// </summary>
    /// <param name="Id"></param>
    /// <param name="projectId"></param>
    /// <returns>TicketRelation by Id</returns>
    [HttpGet("api/projects/{projectId}/[controller]/{Id}")]
    [Authorize(Policy = Permissions.ReadTicketRelation)]
    public async Task<ActionResult<TicketRelationDto>> GetTicketRelation(Guid Id, Guid projectId)
    {
        return Ok(await _ticketRelationService.GetTicketRelationByIdAsync(Id, projectId));
    }

    // POST api/projects/{projectId}/ticketrelations
    /// <summary>
    /// Create TicketRelation - User with WriteTicketRelation permission and matching Project access
    /// </summary>
    /// <param name="ticketRelation"></param>
    /// <param name="projectId"></param>
    /// <returns></returns>
    [HttpPost("api/projects/{projectId}/[controller]")]
    [Authorize(Policy = Permissions.WriteTicketRelation)]
    public async Task<ActionResult> CreateTicketRelation([FromBody] TicketRelationCreateDto ticketRelation, Guid projectId)
    {
        await _ticketRelationService.CreateTicketRelationAsync(ticketRelation, projectId);
        return Ok();
    }

    // PUT api/projects/{projectId}/ticketrelations
    /// <summary>
    /// Update TicketRelation - User with WriteTicketRelation permission and matching Project access
    /// </summary>
    /// <param name="updatedTicketRelation"></param>
    /// <param name="projectId"></param>
    /// <returns></returns>
    [HttpPut("api/projects/{projectId}/[controller]")]
    [Authorize(Policy = Permissions.WriteTicketRelation)]
    public async Task<ActionResult> UpdateTicketRelation([FromBody] TicketRelationUpdateDto updatedTicketRelation, Guid projectId)
    {
        await _ticketRelationService.UpdateTicketRelationAsync(updatedTicketRelation, projectId);
        return Ok();
    }

    // DELETE api/projects/{projectId}/ticketrelations/{Id}
    /// <summary>
    /// Delete TicketRelation by Id - User with WriteTicketRelation permission and matching Project access
    /// </summary>
    /// <param name="Id"></param>
    /// <param name="projectId"></param>
    /// <returns></returns>
    [HttpDelete("api/projects/{projectId}/[controller]/{Id}")]
    [Authorize(Policy = Permissions.WriteTicketRelation)]
    public async Task<ActionResult> DeleteTicketRelation(Guid Id, Guid projectId)
    {
        await _ticketRelationService.DeleteTicketRelationAsync(Id, projectId);
        return Ok();
    }
}