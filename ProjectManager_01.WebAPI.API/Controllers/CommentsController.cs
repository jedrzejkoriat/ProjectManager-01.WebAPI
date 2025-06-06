using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using ProjectManager_01.Application.Constants;
using ProjectManager_01.Application.Contracts.Services;
using ProjectManager_01.Application.DTOs.Comments;

namespace ProjectManager_01.Controllers;

/// <summary>
/// Controller for managing Comments - Admin or User authorization.
/// </summary>
[ApiController]
[Authorize]
public class CommentsController : ControllerBase
{
    private readonly ICommentService _commentService;

    public CommentsController(ICommentService commentService)
    {
        _commentService = commentService;
    }

    // GET: api/comments
    /// <summary>
    /// Get all Comments - Admin only
    /// </summary>
    /// <returns>All Comments</returns>
    [HttpGet("api/[controller]")]
    [Authorize(Roles = Roles.Admin)]
    public async Task<ActionResult<IEnumerable<CommentDto>>> GetComments()
    {
        return Ok(await _commentService.GetAllCommentsAsync());
    }

    // GET api/projects/{projectId}/comments/{id}
    /// <summary>
    /// Get Comment by Id - User with ReadComment permission and matching Project access
    /// </summary>
    /// <param name="id"></param>
    /// <param name="projectId"></param>
    /// <returns>Comment by Id</returns>
    [HttpGet("api/projects/{projectId}/[controller]/{id}")]
    [Authorize(Policy = Permissions.ReadComment)]
    public async Task<ActionResult<CommentDto>> GetComment(Guid id, Guid projectId)
    {
        // TODO: validate if projectId matches the comment's project
        return Ok(await _commentService.GetCommentAsync(id, projectId));
    }

    // POST api/projects/{projectId}/comments
    /// <summary>
    /// Create Comment - User with WriteComment permission and matching Project access
    /// </summary>
    /// <param name="commentCreateDto"></param>
    /// <param name="projectId"></param>
    /// <returns></returns>
    [HttpPost("api/projects/{projectId}/[controller]")]
    [Authorize(Policy = Permissions.WriteComment)]
    public async Task<ActionResult> CreateComment([FromBody] CommentCreateDto commentCreateDto, Guid projectId)
    {
        // TODO: validate if projectId matches the comment's project
        await _commentService.CreateCommentAsync(commentCreateDto, projectId);
        return Ok();
    }

    // PUT api/projects/{projectId}/comments
    /// <summary>
    /// Update Comment - User with matching UserId, WriteComment permission and matching Project access
    /// </summary>
    /// <param name="updatedComment"></param>
    /// <param name="projectId"></param>
    /// <returns></returns>
    [HttpPut("api/projects/{projectId}/[controller]")]
    [Authorize(Policy = Permissions.WriteComment)]
    public ActionResult UpdateComment([FromBody] CommentUpdateDto updatedComment, Guid projectId)
    {
        // TODO: validate if projectId matches the comment's project
        _commentService.UpdateCommentAsync(updatedComment, projectId);
        return Ok();
    }

    // DELETE api/projects/{projectId}/comments/{id}
    /// <summary>
    /// Delete Comment by Id - User with matching UserId, WriteComment permission and matching Project access
    /// </summary>
    /// <param name="id"></param>
    /// <param name="projectId"></param>
    /// <returns></returns>
    [HttpDelete("api/projects/{projectId}/[controller]/{id}")]
    [Authorize(Policy = Permissions.WriteComment)]
    public async Task<ActionResult> DeleteComment(Guid id, Guid projectId)
    {
        // TODO: validate if projectId matches the comment's project
        await _commentService.DeleteCommentAsync(id, projectId);
        return Ok();
    }
}
