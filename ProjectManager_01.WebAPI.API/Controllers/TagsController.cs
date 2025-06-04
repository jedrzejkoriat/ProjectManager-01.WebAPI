using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using ProjectManager_01.Application.Contracts.Services;
using ProjectManager_01.Application.DTOs.Tags;

namespace ProjectManager_01.Controllers;

[EnableRateLimiting("fixedlimit")]
[Route("api/[controller]")]
[ApiController]
public class TagsController : ControllerBase
{
    private readonly ITagService tagService;

    public TagsController(ITagService tagService)
    {
        this.tagService = tagService;
    }

    // GET: api/tags
    /// <summary>
    /// Get all tags
    /// </summary>
    /// <returns>All tags</returns>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<TagDto>>> GetTags()
    {
        try
        {
            return Ok(await tagService.GetAllTagsAsync());
        }
        catch (Exception ex)
        {
            return Problem(ex.Message);
        }
    }

    // GET: api/tags/{id}
    /// <summary>
    /// Get a tag by ID
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id}")]
    public async Task<ActionResult<TagDto>> GetTag(Guid id)
    {
        try
        {
            return Ok(await tagService.GetTagByIdAsync(id));
        }
        catch (Exception ex)
        {
            return Problem(ex.Message);
        }
    }

    // GET: api/tags/project/{projectId}
    /// <summary>
    /// Get all tags by project ID
    /// </summary>
    /// <param name="projectId"></param>
    /// <returns></returns>
    [HttpGet("project/{projectId}")]
    public async Task<ActionResult<IEnumerable<TagDto>>> GetTagsByProjectId(Guid projectId)
    {
        try
        {
            return Ok(await tagService.GetTagsByProjectIdAsync(projectId));
        }
        catch (Exception ex)
        {
            return Problem(ex.Message);
        }
    }

    // POST: api/tags
    /// <summary>
    /// Create a new tag
    /// </summary>
    /// <param name="tag"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<ActionResult> CreateTag([FromBody] TagCreateDto tag)
    {
        try
        {
            await tagService.CreateTagAsync(tag);
            return Ok();
        }
        catch (Exception ex)
        {
            return Problem(ex.Message);
        }
    }

    // PUT: api/tags
    /// <summary>
    /// Update an existing tag
    /// </summary>
    /// <param name="updatedTag"></param>
    /// <returns></returns>
    [HttpPut]
    public async Task<ActionResult> UpdateTag([FromBody] TagUpdateDto updatedTag)
    {
        try
        {
            await tagService.UpdateTagAsync(updatedTag);
            return Ok();
        }
        catch (Exception ex)
        {
            return Problem(ex.Message);
        }
    }

    // DELETE: api/tags/{id}
    /// <summary>
    /// Delete a tag
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteTag(Guid id)
    {
        try
        {
            await tagService.DeleteTagAsync(id);
            return Ok();
        }
        catch (Exception ex)
        {
            return Problem(ex.Message);
        }
    }
}
