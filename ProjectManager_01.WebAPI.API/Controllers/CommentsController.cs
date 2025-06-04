using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using ProjectManager_01.Application.Contracts.Services;
using ProjectManager_01.Application.DTOs.Comments;

namespace ProjectManager_01.Controllers;

[EnableRateLimiting("fixedlimit")]
[Route("api/[controller]")]
[ApiController]
public class CommentsController : ControllerBase
{
    private readonly ICommentService _commentService;

    public CommentsController(ICommentService commentService)
    {
        _commentService = commentService;
    }

    // GET: api/comments
    /// <summary>
    /// Get all comments
    /// </summary>
    /// <returns>All comments</returns>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<CommentDto>>> GetComments()
    {
        return Ok(await _commentService.GetAllCommentsAsync());
    }

    // GET api/comments/{id}
    /// <summary>
    /// Get a comment by ID
    /// </summary>
    /// <param name="id"></param>
    /// <returns>Comment by its id</returns>
    [HttpGet("{id}")]
    public async Task<ActionResult<CommentDto>> GetComment(Guid id)
    {
        return Ok(await _commentService.GetCommentAsync(id));
    }

    // POST api/comments
    /// <summary>
    /// Create a new comment
    /// </summary>
    /// <param name="comment"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<ActionResult> CreateComment([FromBody] CommentCreateDto commentCreateDto)
    {
        await _commentService.CreateCommentAsync(commentCreateDto);
        return Ok();
    }

    // PUT api/comments
    /// <summary>
    /// Update an existing comment
    /// </summary>
    /// <param name="updatedComment"></param>
    /// <returns></returns>
    [HttpPut]
    public ActionResult UpdateComment([FromBody] CommentUpdateDto updatedComment)
    {
        _commentService.UpdateCommentAsync(updatedComment);
        return Ok();
    }

    // DELETE api/comments/{id}
    /// <summary>
    /// Delete a comment
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteComment(Guid id)
    {
        await _commentService.DeleteCommentAsync(id);
        return Ok();
    }
}
