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
[EnableRateLimiting("fixedlimit")]
[Route("api/[controller]")]
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
    [HttpGet]
    [Authorize(Roles = Roles.Admin)]
    public async Task<ActionResult<IEnumerable<CommentDto>>> GetComments()
    {
        return Ok(await _commentService.GetAllCommentsAsync());
    }

    // GET api/comments/{id}
    /// <summary>
    /// Get Comment by Id - User with ReadComment permission and matching Project access
    /// </summary>
    /// <param name="id"></param>
    /// <returns>Comment by Id</returns>
    [HttpGet("{id}")]
    [Authorize(Policy = Permissions.ReadComment)]
    public async Task<ActionResult<CommentDto>> GetComment(Guid id)
    {
        return Ok(await _commentService.GetCommentAsync(id));
    }

    // POST api/comments
    /// <summary>
    /// Create Comment - User with WriteComment permission and matching Project access
    /// </summary>
    /// <param name="commentCreateDto"></param>
    /// <returns></returns>
    [HttpPost]
    [Authorize(Policy = Permissions.WriteComment)]
    public async Task<ActionResult> CreateComment([FromBody] CommentCreateDto commentCreateDto)
    {
        await _commentService.CreateCommentAsync(commentCreateDto);
        return Ok();
    }

    // PUT api/comments
    /// <summary>
    /// Update Comment - User with matching UserId, WriteComment permission and matching Project access
    /// </summary>
    /// <param name="updatedComment"></param>
    /// <returns></returns>
    [HttpPut]
    [Authorize(Policy = Permissions.WriteComment)]
    public ActionResult UpdateComment([FromBody] CommentUpdateDto updatedComment)
    {
        _commentService.UpdateCommentAsync(updatedComment);
        return Ok();
    }

    // DELETE api/comments/{id}
    /// <summary>
    /// Delete Comment by Id - User with matching UserId, WriteComment permission and matching Project access
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete("{id}")]
    [Authorize(Policy = Permissions.WriteComment)]
    public async Task<ActionResult> DeleteComment(Guid id)
    {
        await _commentService.DeleteCommentAsync(id);
        return Ok();
    }
}
