using System.Threading.Tasks;
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
    private readonly ICommentService commentService;
    public CommentsController(ICommentService commentService)
    {
        this.commentService = commentService;
    }

    private static List<CommentDto> comments = new List<CommentDto>
    {
        new CommentDto { Id = Guid.NewGuid(), TicketId = Guid.NewGuid(), Content = "First comment", CreatedAt = DateTime.Now, UserId = Guid.NewGuid() },
        new CommentDto { Id = Guid.NewGuid(), TicketId = Guid.NewGuid(), Content = "Second comment", CreatedAt = DateTime.Now, UserId = Guid.NewGuid() }
    };
    
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
            return Ok(await commentService.GetCommentsAsync());
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
        var comment = comments.FirstOrDefault(c => c.Id == id);

        if (comment == null)
            return NotFound();

        try
        {
            return Ok(await commentService.GetCommentAsync(id));
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
    public async Task<ActionResult> Post([FromBody] CommentCreateDto commentCreateDto)
    {
        try
        {
            await commentService.CreateCommentAsync(commentCreateDto);
            return Ok();
        }
        catch (Exception ex)
        {
            return Problem(ex.Message);
        }
    }

    // PUT api/comments/{id}
    /// <summary>
    /// Update an existing comment
    /// </summary>
    /// <param name="updatedComment"></param>
    /// <returns></returns>
    [HttpPut]
    public ActionResult Put([FromBody] CommentUpdateDto updatedComment)
    {
        try
        {
            commentService.UpdateCommentAsync(updatedComment);
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
    public async Task<ActionResult> Delete(Guid id)
    {
        var comment = comments.FirstOrDefault(c => c.Id == id);

        if (comment == null)
            return NotFound();

        try
        {
            await commentService.DeleteCommentAsync(id);
            return Ok();
        }
        catch (Exception ex)
        {
            return Problem(ex.Message);
        }
    }
}
