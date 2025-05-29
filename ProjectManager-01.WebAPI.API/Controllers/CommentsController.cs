using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using ProjectManager_01.API.DTOs;

namespace ProjectManager_01.Controllers;

[EnableRateLimiting("fixedlimit")]
[Route("api/[controller]")]
[ApiController]
public class CommentsController : ControllerBase
{
    private static List<CommentDTO> comments = new List<CommentDTO>
        {
            new CommentDTO { Id = Guid.NewGuid(), TicketId = Guid.NewGuid(), Content = "First comment", CreatedAt = DateTime.Now, UserId = Guid.NewGuid() },
            new CommentDTO { Id = Guid.NewGuid(), TicketId = Guid.NewGuid(), Content = "Second comment", CreatedAt = DateTime.Now, UserId = Guid.NewGuid() }
        };

    // GET: api/comments
    /// <summary>
    /// Get all comments
    /// </summary>
    /// <returns>All comments</returns>
    [HttpGet]
    public ActionResult<IEnumerable<CommentDTO>> GetComments()
    {
        return Ok(comments);
    }

    // GET api/comments/{id}
    /// <summary>
    /// Get a comment by ID
    /// </summary>
    /// <param name="id"></param>
    /// <returns>Comment by its id</returns>
    [HttpGet("{id}")]
    public ActionResult<CommentDTO> GetComment(Guid id)
    {
        var comment = comments.FirstOrDefault(c => c.Id == id);

        if (comment == null) 
            return NotFound();

        return Ok(comment);
    }

    // POST api/comments
    /// <summary>
    /// Create a new comment
    /// </summary>
    /// <param name="comment"></param>
    /// <returns></returns>
    [HttpPost]
    public ActionResult Post([FromBody] CommentDTO comment)
    {
        comment.Id = Guid.NewGuid();
        comments.Add(comment);

        return CreatedAtAction(nameof(GetComment), new { id = comment.Id }, comment);
    }

    // PUT api/comments/{id}
    /// <summary>
    /// Update an existing comment
    /// </summary>
    /// <param name="id"></param>
    /// <param name="updatedComment"></param>
    /// <returns></returns>
    [HttpPut("{id}")]
    public ActionResult Put(Guid id, [FromBody] CommentDTO updatedComment)
    {
        var comment = comments.FirstOrDefault(c => c.Id == id);

        if (comment == null) 
            return NotFound();

        comment.TicketId = updatedComment.TicketId;
        comment.Content = updatedComment.Content;
        comment.CreatedAt = updatedComment.CreatedAt;

        return NoContent();
    }

    // DELETE api/comments/{id}
    /// <summary>
    /// Delete a comment
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete("{id}")]
    public ActionResult Delete(Guid id)
    {
        var comment = comments.FirstOrDefault(c => c.Id == id);

        if (comment == null) 
            return NotFound();

        comments.Remove(comment);

        return NoContent();
    }
}
