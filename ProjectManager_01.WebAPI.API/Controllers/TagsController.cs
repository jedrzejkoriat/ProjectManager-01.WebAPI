using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using ProjectManager_01.Application.Constants;
using ProjectManager_01.Application.Contracts.Services;
using ProjectManager_01.Application.DTOs.Tags;

namespace ProjectManager_01.Controllers;

/// <summary>
/// Controller for managin Tags - Admin or User authorization.
/// </summary>
[EnableRateLimiting("fixedlimit")]
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
    /// Get all Tags - Admin only
    /// </summary>
    /// <returns>All Tags</returns>
    [HttpGet("api/[controller]")]
    [Authorize(Roles = Roles.Admin)]
    public async Task<ActionResult<IEnumerable<TagDto>>> GetTags()
    {
        return Ok(await _tagService.GetAllTagsAsync());
    }

    // GET: api/projects/{projectId}/tags/{id}
    /// <summary>
    /// Get Tag by Id - User with ReadTag permission and matching Project access
    /// </summary>
    /// <param name="id"></param>
    /// <param name="projectId"></param>
    /// <returns>Tag by Id</returns>
    [HttpGet("api/projects/{projectId}/[controller]/{id}")]
    [Authorize(Policy = Permissions.ReadTag)]
    public async Task<ActionResult<TagDto>> GetTag(Guid id, Guid projectId)
    {
        // TODO: validate projectId
        return Ok(await _tagService.GetTagByIdAsync(id));
    }

    // GET: api/projects/{projectId}/tags
    /// <summary>
    /// Get Tags by ProjectId - User with ReadTag permission and matching Project access
    /// </summary>
    /// <param name="projectId"></param>
    /// <returns>All project Tags</returns>
    [HttpGet("api/projects/{projectId}/[controller]/")]
    [Authorize(Policy = Permissions.ReadTag)]
    public async Task<ActionResult<IEnumerable<TagDto>>> GetTagsByProjectId(Guid projectId)
    {
        return Ok(await _tagService.GetTagsByProjectIdAsync(projectId));
    }

    // POST: api/projects/{projectId}/tags
    /// <summary>
    /// Create Tag - User with WriteTag permission and matching Project access
    /// </summary>
    /// <param name="tag"></param>
    /// <param name="projectId"></param>"
    /// <returns></returns>
    [HttpPost("api/projects/{projectId}/[controller]/")]
    [Authorize(Policy = Permissions.WriteTag)]
    public async Task<ActionResult> CreateTag([FromBody] TagCreateDto tag, Guid projectId)
    {
        // TODO: validate projectId
        await _tagService.CreateTagAsync(tag);
        return Ok();
    }

    // PUT: api/projects/{projectId}/tags
    /// <summary>
    /// Update Tag - User with WriteTag permission and matching Project access
    /// </summary>
    /// <param name="updatedTag"></param>
    /// <param name="projectId"></param>"
    /// <returns></returns>
    [HttpPut("api/projects/{projectId}/[controller]/")]
    [Authorize(Policy = Permissions.WriteTag)]
    public async Task<ActionResult> UpdateTag([FromBody] TagUpdateDto updatedTag, Guid projectId)
    {
        // TODO: validate projectId
        await _tagService.UpdateTagAsync(updatedTag);
        return Ok();
    }

    // DELETE: api/projects/{projectId}/tags/{id}
    /// <summary>
    /// Delete Tag by Id - User with WriteTag permission and matching Project access
    /// </summary>
    /// <param name="id"></param>
    /// <param name="projectId"></param>"
    /// <returns></returns>
    [HttpDelete("api/projects/{projectId}/[controller]/{id}")]
    [Authorize(Policy = Permissions.WriteTag)]
    public async Task<ActionResult> DeleteTag(Guid id, Guid projectId)
    {
        // TODO: validate projectId
        await _tagService.DeleteTagAsync(id);
        return Ok();
    }
}
