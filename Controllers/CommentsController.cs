using Microsoft.AspNetCore.Mvc;
using ProjectManager_01.WebAPI.Data;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ProjectManager_01.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private static List<Comment> comments = new List<Comment>
        {
            new Comment { Id = 1, TicketId = 1, Content = "First comment", CreatedAt = DateTime.Now },
            new Comment { Id = 2, TicketId = 2, Content = "Second comment", CreatedAt = DateTime.Now }
        };

        [HttpGet]
        public ActionResult<IEnumerable<Comment>> GetComments() => Ok(comments);

        [HttpGet("{id}")]
        public ActionResult<Comment> GetComment(int id)
        {
            var comment = comments.FirstOrDefault(c => c.Id == id);
            if (comment == null) return NotFound();
            return Ok(comment);
        }

        [HttpPost]
        public ActionResult Post([FromBody] Comment comment)
        {
            comment.Id = comments.Max(c => c.Id) + 1;
            comments.Add(comment);
            return CreatedAtAction(nameof(GetComment), new { id = comment.Id }, comment);
        }

        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] Comment updatedComment)
        {
            var comment = comments.FirstOrDefault(c => c.Id == id);
            if (comment == null) return NotFound();

            comment.TicketId = updatedComment.TicketId;
            comment.Content = updatedComment.Content;
            comment.CreatedAt = updatedComment.CreatedAt;
            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var comment = comments.FirstOrDefault(c => c.Id == id);
            if (comment == null) return NotFound();

            comments.Remove(comment);
            return NoContent();
        }
    }
}
