using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectManager_01.WebAPI.Data;

namespace ProjectManager_01.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketTypesController : ControllerBase
    {
        private static List<TicketType> ticketTypes = new List<TicketType>
        {
            new TicketType { Id = 1, Name = "Bug" },
            new TicketType { Id = 2, Name = "Task" }
        };

        [HttpGet]
        public ActionResult<IEnumerable<TicketType>> GetTicketTypes() => Ok(ticketTypes);

        [HttpGet("{id}")]
        public ActionResult<TicketType> GetTicketType(int id)
        {
            var ticketType = ticketTypes.FirstOrDefault(t => t.Id == id);
            if (ticketType == null) return NotFound();
            return Ok(ticketType);
        }

        [HttpPost]
        public ActionResult Post([FromBody] TicketType ticketType)
        {
            ticketType.Id = ticketTypes.Max(t => t.Id) + 1;
            ticketTypes.Add(ticketType);
            return CreatedAtAction(nameof(GetTicketType), new { id = ticketType.Id }, ticketType);
        }

        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] TicketType updatedTicketType)
        {
            var ticketType = ticketTypes.FirstOrDefault(t => t.Id == id);
            if (ticketType == null) return NotFound();

            ticketType.Name = updatedTicketType.Name;
            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var ticketType = ticketTypes.FirstOrDefault(t => t.Id == id);
            if (ticketType == null) return NotFound();

            ticketTypes.Remove(ticketType);
            return NoContent();
        }
    }
}
