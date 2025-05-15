using Microsoft.AspNetCore.Mvc;
using ProjectManager_01.WebAPI.Data;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ProjectManager_01.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PrioritiesController : ControllerBase
    {
        private static List<Priority> priorities = new List<Priority>
        {
            new Priority { Id = 1, Name = "Low", Level = 1 },
            new Priority { Id = 2, Name = "High", Level = 5 }
        };

        [HttpGet]
        public ActionResult<IEnumerable<Priority>> GetPriorities() => Ok(priorities);

        [HttpGet("{id}")]
        public ActionResult<Priority> GetPriority(int id)
        {
            var priority = priorities.FirstOrDefault(p => p.Id == id);
            if (priority == null) return NotFound();
            return Ok(priority);
        }

        [HttpPost]
        public ActionResult Post([FromBody] Priority priority)
        {
            priority.Id = priorities.Max(p => p.Id) + 1;
            priorities.Add(priority);
            return CreatedAtAction(nameof(GetPriority), new { id = priority.Id }, priority);
        }

        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] Priority updatedPriority)
        {
            var priority = priorities.FirstOrDefault(p => p.Id == id);
            if (priority == null) return NotFound();

            priority.Name = updatedPriority.Name;
            priority.Level = updatedPriority.Level;
            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var priority = priorities.FirstOrDefault(p => p.Id == id);
            if (priority == null) return NotFound();

            priorities.Remove(priority);
            return NoContent();
        }
    }
}
