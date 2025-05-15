using Microsoft.AspNetCore.Mvc;
using ProjectManager_01.WebAPI.Data;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ProjectManager_01.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResolutionsController : ControllerBase
    {
        private static List<Resolution> resolutions = new List<Resolution>
        {
            new Resolution { Id = 1, Name = "Fixed" },
            new Resolution { Id = 2, Name = "Won't Fix" }
        };

        [HttpGet]
        public ActionResult<IEnumerable<Resolution>> GetResolutions() => Ok(resolutions);

        [HttpGet("{id}")]
        public ActionResult<Resolution> GetResolution(int id)
        {
            var resolution = resolutions.FirstOrDefault(r => r.Id == id);
            if (resolution == null) return NotFound();
            return Ok(resolution);
        }

        [HttpPost]
        public ActionResult Post([FromBody] Resolution resolution)
        {
            resolution.Id = resolutions.Max(r => r.Id) + 1;
            resolutions.Add(resolution);
            return CreatedAtAction(nameof(GetResolution), new { id = resolution.Id }, resolution);
        }

        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] Resolution updatedResolution)
        {
            var resolution = resolutions.FirstOrDefault(r => r.Id == id);
            if (resolution == null) return NotFound();

            resolution.Name = updatedResolution.Name;
            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var resolution = resolutions.FirstOrDefault(r => r.Id == id);
            if (resolution == null) return NotFound();

            resolutions.Remove(resolution);
            return NoContent();
        }
    }
}
