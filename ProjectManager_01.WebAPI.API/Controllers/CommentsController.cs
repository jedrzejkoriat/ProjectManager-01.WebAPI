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
        try
        {
            return Ok(await _commentService.GetAllCommentsAsync());
        }
        catch (Exception ex)
        {
            return Problem(ex.Message);
        }
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
        try
        {
            return Ok(await _commentService.GetCommentAsync(id));
        }
        catch (Exception ex)
        {
            return Problem(ex.Message);
        }
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
        try
        {
            await _commentService.CreateCommentAsync(commentCreateDto);
            return Ok();
        }
        catch (Exception ex)
        {
            return Problem(ex.Message);
        }
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
        try
        {
            _commentService.UpdateCommentAsync(updatedComment);
            return Ok();
        }
        catch (Exception ex)
        {
            return Problem(ex.Message);
        }
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
        try
        {
            await _commentService.DeleteCommentAsync(id);
            return Ok();
        }
        catch (Exception ex)
        {
            return Problem(ex.Message);
        }
    }
}
