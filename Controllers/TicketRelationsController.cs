using Microsoft.AspNetCore.Mvc;
using ProjectManager_01.WebAPI.Data;

namespace ProjectManager_01.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TicketRelationsController : ControllerBase
{
    private static List<TicketRelation> ticketRelations = new List<TicketRelation>
    {
        new TicketRelation { TargetId = Guid.NewGuid(), SourceId = Guid.NewGuid(), RelationType = Enums.RelationType.Blocks },
        new TicketRelation { TargetId = Guid.NewGuid(), SourceId = Guid.NewGuid(), RelationType = Enums.RelationType.Relates }
    };

    // GET: api/ticketrelations
    /// <summary>
    /// Get all ticket relations
    /// </summary>
    /// <returns>All ticket relations</returns>
    [HttpGet]
    public ActionResult<IEnumerable<TicketRelation>> GetTicketRelations()
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
    public ActionResult<TicketRelation> GetTicketRelation(Guid targetId, Guid sourceId)
    {
        TicketRelation ticketRelation = ticketRelations.FirstOrDefault(tr => tr.TargetId == targetId && tr.SourceId == sourceId);

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
    public ActionResult Post([FromBody] TicketRelation ticketRelation)
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
    public ActionResult Put(Guid targetId, Guid sourceId, [FromBody] TicketRelation updatedTicketRelation)
    {
        TicketRelation ticketRelation = ticketRelations.FirstOrDefault(tr => tr.TargetId == targetId && tr.SourceId == sourceId);

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
        TicketRelation ticketRelation = ticketRelations.FirstOrDefault(tr => tr.TargetId == targetId && tr.SourceId == sourceId);

        if (ticketRelation == null)
            return NotFound();

        ticketRelations.Remove(ticketRelation);

        return NoContent();
    }
}