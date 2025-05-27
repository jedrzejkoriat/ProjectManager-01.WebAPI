using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using ProjectManager_01.WebAPI.Application.DTOs;

namespace ProjectManager_01.WebAPI.Controllers;

[EnableRateLimiting("fixedlimit")]
[Route("api/[controller]")]
[ApiController]
public class TagsController : ControllerBase
{
    private static List<TagDTO> tags = new List<TagDTO>
        {
            new TagDTO { Id = Guid.NewGuid(), Name = "Bug" },
            new TagDTO { Id = Guid.NewGuid(), Name = "Feature" }
        };

    // GET: api/tags
    /// <summary>
    /// Get all tags
    /// </summary>
    /// <returns>All tags</returns>
    [HttpGet]
    public ActionResult<IEnumerable<TagDTO>> GetTags()
    {
        return Ok(tags);
    }

    // GET: api/tags/{id}
    /// <summary>
    /// Get a tag by ID
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id}")]
    public ActionResult<TagDTO> GetTag(Guid id)
    {
        var tag = tags.FirstOrDefault(t => t.Id == id);

        if (tag == null) 
            return NotFound();

        return Ok(tag);
    }

    // POST: api/tags
    /// <summary>
    /// Create a new tag
    /// </summary>
    /// <param name="tag"></param>
    /// <returns></returns>
    [HttpPost]
    public ActionResult Post([FromBody] TagDTO tag)
    {
        tag.Id = Guid.NewGuid();
        tags.Add(tag);

        return CreatedAtAction(nameof(GetTag), new { id = tag.Id }, tag);
    }

    // PUT: api/tags/{id}
    /// <summary>
    /// Update an existing tag
    /// </summary>
    /// <param name="id"></param>
    /// <param name="updatedTag"></param>
    /// <returns></returns>
    [HttpPut("{id}")]
    public ActionResult Put(Guid id, [FromBody] TagDTO updatedTag)
    {
        var tag = tags.FirstOrDefault(t => t.Id == id);

        if (tag == null) 
            return NotFound();

        tag.Name = updatedTag.Name;

        return NoContent();
    }

    // DELETE: api/tags/{id}
    /// <summary>
    /// Delete a tag
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete("{id}")]
    public ActionResult Delete(Guid id)
    {
        var tag = tags.FirstOrDefault(t => t.Id == id);

        if (tag == null) 
            return NotFound();

        tags.Remove(tag);

        return NoContent();
    }
}
