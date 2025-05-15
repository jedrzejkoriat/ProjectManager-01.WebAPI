using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectManager_01.WebAPI.Data;

namespace ProjectManager_01.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatusesController : ControllerBase
    {
        private static List<Status> statuses = new List<Status>
        {
            new Status { Id = 1, Name = "Open" },
            new Status { Id = 2, Name = "Closed" }
        };

        // GET: api/statuses
        /// <summary>
        /// Get all statuses
        /// </summary>
        /// <returns>All statuses</returns>
        [HttpGet]
        public ActionResult<IEnumerable<Status>> GetStatuses()
        {
            return Ok(statuses);
        }

        // GET: api/statuses/{id}
        /// <summary>
        /// Get a status by ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public ActionResult<Status> GetStatus(int id)
        {
            var status = statuses.FirstOrDefault(s => s.Id == id);
            if (status == null) return NotFound();
            return Ok(status);
        }

        // POST: api/statuses
        /// <summary>
        /// Create a new status
        /// </summary>
        /// <param name="status"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Post([FromBody] Status status)
        {
            status.Id = statuses.Max(s => s.Id) + 1;
            statuses.Add(status);
            return CreatedAtAction(nameof(GetStatus), new { id = status.Id }, status);
        }

        // PUT: api/statuses/{id}
        /// <summary>
        /// Update an existing status
        /// </summary>
        /// <param name="id"></param>
        /// <param name="updatedStatus"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] Status updatedStatus)
        {
            var status = statuses.FirstOrDefault(s => s.Id == id);
            if (status == null) return NotFound();

            status.Name = updatedStatus.Name;
            return NoContent();
        }

        // DELETE: api/statuses/{id}
        /// <summary>
        /// Delete a status
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var status = statuses.FirstOrDefault(s => s.Id == id);
            if (status == null) return NotFound();

            statuses.Remove(status);
            return NoContent();
        }
    }
}
