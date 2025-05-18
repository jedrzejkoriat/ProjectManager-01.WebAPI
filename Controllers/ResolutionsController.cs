using Microsoft.AspNetCore.Mvc;
using ProjectManager_01.WebAPI.Data;

namespace ProjectManager_01.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ResolutionsController : ControllerBase
{
    private static List<Resolution> resolutions = new List<Resolution>
        {
            new Resolution { Id = 1, Name = "Fixed" },
            new Resolution { Id = 2, Name = "Won't Fix" }
        };
    // GET api/resolutions
    /// <summary>
    /// Get all resolutions
    /// </summary>
    /// <returns>All resolutions</returns>
    [HttpGet]
    public ActionResult<IEnumerable<Resolution>> GetResolutions()
    {
        return Ok(resolutions);
    }

    // GET api/resolutions/{id}
    /// <summary>
    /// Get a resolution by ID
    /// </summary>
    /// <param name="id"></param>
    /// <returns>Resolution by its id</returns>
    [HttpGet("{id}")]
    public ActionResult<Resolution> GetResolution(int id)
    {
        var resolution = resolutions.FirstOrDefault(r => r.Id == id);
        if (resolution == null) return NotFound();
        return Ok(resolution);
    }

    // POST api/resolutions
    /// <summary>
    /// Create a new resolution
    /// </summary>
    /// <param name="resolution"></param>
    /// <returns></returns>
    [HttpPost]
    public ActionResult Post([FromBody] Resolution resolution)
    {
        resolution.Id = resolutions.Max(r => r.Id) + 1;
        resolutions.Add(resolution);
        return CreatedAtAction(nameof(GetResolution), new { id = resolution.Id }, resolution);
    }

    // PUT api/resolutions/{id}
    /// <summary>
    /// Update an existing resolution
    /// </summary>
    /// <param name="id"></param>
    /// <param name="updatedResolution"></param>
    /// <returns></returns>
    [HttpPut("{id}")]
    public ActionResult Put(int id, [FromBody] Resolution updatedResolution)
    {
        var resolution = resolutions.FirstOrDefault(r => r.Id == id);
        if (resolution == null) return NotFound();

        resolution.Name = updatedResolution.Name;
        return NoContent();
    }

    // DELETE api/resolutions/{id}
    /// <summary>
    /// Delete a resolution
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete("{id}")]
    public ActionResult Delete(int id)
    {
        var resolution = resolutions.FirstOrDefault(r => r.Id == id);
        if (resolution == null) return NotFound();

        resolutions.Remove(resolution);
        return NoContent();
    }
}
