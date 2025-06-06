using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using ProjectManager_01.Application.Constants;
using ProjectManager_01.Application.Contracts.Services;
using ProjectManager_01.Application.DTOs.Tags;

namespace ProjectManager_01.Controllers;

[EnableRateLimiting("fixedlimit")]
[Route("api/[controller]")]
[ApiController]
[Authorize]
public class TagsController : ControllerBase
{
    private readonly ITagService _tagService;

    public TagsController(ITagService tagService)
    {
        _tagService = tagService;
    }

    // GET: api/tags
    /// <summary>
    /// Get all tags
    /// </summary>
    /// <returns>All tags</returns>
    [HttpGet]
    [Authorize(Roles = Roles.Admin)]
    public async Task<ActionResult<IEnumerable<TagDto>>> GetTags()
    {
        return Ok(await _tagService.GetAllTagsAsync());
    }

    // GET: api/tags/{id}
    /// <summary>
    /// Get a tag by ID
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id}")]
    [Authorize(Policy = Permissions.ReadTag)]
    public async Task<ActionResult<TagDto>> GetTag(Guid id)
    {
        return Ok(await _tagService.GetTagByIdAsync(id));
    }

    // GET: api/tags/project/{projectId}
    /// <summary>
    /// Get all tags by project ID
    /// </summary>
    /// <param name="projectId"></param>
    /// <returns></returns>
    [HttpGet("project/{projectId}")]
    [Authorize(Policy = Permissions.ReadTag)]
    public async Task<ActionResult<IEnumerable<TagDto>>> GetTagsByProjectId(Guid projectId)
    {
        return Ok(await _tagService.GetTagsByProjectIdAsync(projectId));
    }

    // POST: api/tags
    /// <summary>
    /// Create a new tag
    /// </summary>
    /// <param name="tag"></param>
    /// <returns></returns>
    [HttpPost]
    [Authorize(Policy = Permissions.WriteTag)]
    public async Task<ActionResult> CreateTag([FromBody] TagCreateDto tag)
    {
        await _tagService.CreateTagAsync(tag);
        return Ok();
    }

    // PUT: api/tags
    /// <summary>
    /// Update an existing tag
    /// </summary>
    /// <param name="updatedTag"></param>
    /// <returns></returns>
    [HttpPut]
    [Authorize(Policy = Permissions.WriteTag)]
    public async Task<ActionResult> UpdateTag([FromBody] TagUpdateDto updatedTag)
    {
        await _tagService.UpdateTagAsync(updatedTag);
        return Ok();
    }

    // DELETE: api/tags/{id}
    /// <summary>
    /// Delete a tag
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete("{id}")]
    [Authorize(Policy = Permissions.WriteTag)]
    public async Task<ActionResult> DeleteTag(Guid id)
    {
        await _tagService.DeleteTagAsync(id);
        return Ok();
    }
}
