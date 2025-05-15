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

        [HttpGet]
        public ActionResult<IEnumerable<Tag>> GetTags() => Ok(tags);

        [HttpGet("{id}")]
        public ActionResult<Tag> GetTag(int id)
        {
            var tag = tags.FirstOrDefault(t => t.Id == id);
            if (tag == null) return NotFound();
            return Ok(tag);
        }

        [HttpPost]
        public ActionResult Post([FromBody] Tag tag)
        {
            tag.Id = tags.Max(t => t.Id) + 1;
            tags.Add(tag);
            return CreatedAtAction(nameof(GetTag), new { id = tag.Id }, tag);
        }

        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] Tag updatedTag)
        {
            var tag = tags.FirstOrDefault(t => t.Id == id);
            if (tag == null) return NotFound();

            tag.Name = updatedTag.Name;
            return NoContent();
        }

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
