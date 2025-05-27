using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using ProjectManager_01.WebAPI.Application.DTOs;

namespace ProjectManager_01.WebAPI.Controllers;

[EnableRateLimiting("fixedlimit")]
[Route("api/[controller]")]
[ApiController]
public class TicketRelationsController : ControllerBase
{
    private static List<TicketRelationDTO> ticketRelations = new List<TicketRelationDTO>
    {
        new TicketRelationDTO { Id = Guid.NewGuid(), TargetId = Guid.NewGuid(), SourceId = Guid.NewGuid(), RelationType = 1 },
        new TicketRelationDTO { Id = Guid.NewGuid(), TargetId = Guid.NewGuid(), SourceId = Guid.NewGuid(), RelationType = 1 }
    };

    // GET: api/ticketrelations
    /// <summary>
    /// Get all ticket relations
    /// </summary>
    /// <returns>All ticket relations</returns>
    [HttpGet]
    public ActionResult<IEnumerable<TicketRelationDTO>> GetTicketRelations()
    {
        return Ok(ticketRelations);
    }

    // GET api/ticketrelations/{targetId}/{sourceId}
    /// <summary>
    /// Get a ticket relation by TargetId and SourceId
    /// </summary>
    /// <param name="targetId"></param>
    /// <param name="sourceId"></param>
    /// <returns>Ticket relation by its composite key</returns>
    [HttpGet("{targetId}/{sourceId}")]
    public ActionResult<TicketRelationDTO> GetTicketRelation(Guid targetId, Guid sourceId)
    {
        TicketRelationDTO ticketRelation = ticketRelations.FirstOrDefault(tr => tr.TargetId == targetId && tr.SourceId == sourceId);

        if (ticketRelation == null)
            return NotFound();

        return Ok(ticketRelation);
    }

    // POST api/ticketrelations
    /// <summary>
    /// Create a new ticket relation
    /// </summary>
    /// <param name="ticketRelation"></param>
    /// <returns></returns>
    [HttpPost]
    public ActionResult Post([FromBody] TicketRelationDTO ticketRelation)
    {
        ticketRelations.Add(ticketRelation);

        return CreatedAtAction(nameof(GetTicketRelation), new { targetId = ticketRelation.TargetId, sourceId = ticketRelation.SourceId }, ticketRelation);
    }

    // PUT api/ticketrelations/{targetId}/{sourceId}
    /// <summary>
    /// Update an existing ticket relation
    /// </summary>
    /// <param name="targetId"></param>
    /// <param name="sourceId"></param>
    /// <param name="updatedTicketRelation"></param>
    /// <returns></returns>
    [HttpPut("{targetId}/{sourceId}")]
    public ActionResult Put(Guid targetId, Guid sourceId, [FromBody] TicketRelationDTO updatedTicketRelation)
    {
        TicketRelationDTO ticketRelation = ticketRelations.FirstOrDefault(tr => tr.TargetId == targetId && tr.SourceId == sourceId);

        if (ticketRelation == null)
            return NotFound();

        ticketRelation.RelationType = updatedTicketRelation.RelationType;

        return NoContent();
    }

    // DELETE api/ticketrelations/{targetId}/{sourceId}
    /// <summary>
    /// Delete a ticket relation
    /// </summary>
    /// <param name="targetId"></param>
    /// <param name="sourceId"></param>
    /// <returns></returns>
    [HttpDelete("{targetId}/{sourceId}")]
    public ActionResult Delete(Guid targetId, Guid sourceId)
    {
        TicketRelationDTO ticketRelation = ticketRelations.FirstOrDefault(tr => tr.TargetId == targetId && tr.SourceId == sourceId);

        if (ticketRelation == null)
            return NotFound();

        ticketRelations.Remove(ticketRelation);

        return NoContent();
    }
}