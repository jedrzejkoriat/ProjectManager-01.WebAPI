using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using ProjectManager_01.WebAPI.Domain.Models;

namespace ProjectManager_01.WebAPI.Controllers;

[EnableRateLimiting("fixedlimit")]
[Route("api/[controller]")]
[ApiController]
public class PrioritiesController : ControllerBase
{
    private static List<Priority> priorities = new List<Priority>
        {
            new Priority { Id = Guid.NewGuid(), Name = "Low", Level = 1 },
            new Priority { Id = Guid.NewGuid(), Name = "High", Level = 5 }
        };

    // GET api/priorities
    /// <summary>
    /// Get all priorities
    /// </summary>
    /// <returns>All priorities</returns>
    [HttpGet]
    public ActionResult<IEnumerable<Priority>> GetPriorities()
    {
        return Ok(priorities);
    }

    // GET api/priorities/{id}
    /// <summary>
    /// Get a priority by ID
    /// </summary>
    /// <param name="id"></param>
    /// <returns>Priority by id</returns>
    [HttpGet("{id}")]
    public ActionResult<Priority> GetPriority(Guid id)
    {
        var priority = priorities.FirstOrDefault(p => p.Id == id);

        if (priority == null) 
            return NotFound();

        return Ok(priority);
    }

    // POST api/priorities
    /// <summary>
    /// Create a new priority
    /// </summary>
    /// <param name="priority"></param>
    /// <returns></returns>
    [HttpPost]
    public ActionResult Post([FromBody] Priority priority)
    {
        priority.Id = Guid.NewGuid();
        priorities.Add(priority);

        return CreatedAtAction(nameof(GetPriority), new { id = priority.Id }, priority);
    }

    // PUT api/priorities/{id}
    /// <summary>
    /// Update an existing priority
    /// </summary>
    /// <param name="id"></param>
    /// <param name="updatedPriority"></param>
    /// <returns></returns>
    [HttpPut("{id}")]
    public ActionResult Put(Guid id, [FromBody] Priority updatedPriority)
    {
        var priority = priorities.FirstOrDefault(p => p.Id == id);

        if (priority == null) 
            return NotFound();

        priority.Name = updatedPriority.Name;
        priority.Level = updatedPriority.Level;

        return NoContent();
    }

    // DELETE api/priorities/{id}
    /// <summary>
    /// Delete a priority
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete("{id}")]
    public ActionResult Delete(Guid id)
    {
        var priority = priorities.FirstOrDefault(p => p.Id == id);

        if (priority == null) 
            return NotFound();

        priorities.Remove(priority);

        return NoContent();
    }
}