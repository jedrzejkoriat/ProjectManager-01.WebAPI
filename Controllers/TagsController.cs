using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectManager_01.WebAPI.Data;

namespace ProjectManager_01.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TagsController : ControllerBase
    {
        private static List<Tag> tags = new List<Tag>
        {
            new Tag { Id = 1, Name = "Bug" },
            new Tag { Id = 2, Name = "Feature" }
        };

        // GET: api/tags
        /// <summary>
        /// Get all tags
        /// </summary>
        /// <returns>All tags</returns>
        [HttpGet]
        public ActionResult<IEnumerable<Tag>> GetTags()
        {
            return Ok(tags);
        }

        // GET: api/tags/{id}
        /// <summary>
        /// Get a tag by ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public ActionResult<Tag> GetTag(int id)
        {
            var tag = tags.FirstOrDefault(t => t.Id == id);
            if (tag == null) return NotFound();
            return Ok(tag);
        }

        // POST: api/tags
        /// <summary>
        /// Create a new tag
        /// </summary>
        /// <param name="tag"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Post([FromBody] Tag tag)
        {
            tag.Id = tags.Max(t => t.Id) + 1;
            tags.Add(tag);
            return CreatedAtAction(nameof(GetTag), new { id = tag.Id }, tag);
        }

        // PUT: api/tags/{id}
        /// <summary>
        /// Update an existing tag
        /// </summary>
        /// <param name="id"></param>
        /// <param name="updatedTag"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] Tag updatedTag)
        {
            var tag = tags.FirstOrDefault(t => t.Id == id);
            if (tag == null) return NotFound();

            tag.Name = updatedTag.Name;
            return NoContent();
        }

        // DELETE: api/tags/{id}
        /// <summary>
        /// Delete a tag
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var tag = tags.FirstOrDefault(t => t.Id == id);
            if (tag == null) return NotFound();

            tags.Remove(tag);
            return NoContent();
        }
    }
}
