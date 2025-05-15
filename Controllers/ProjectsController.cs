using Microsoft.AspNetCore.Mvc;
using ProjectManager_01.WebAPI.Data;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ProjectManager_01.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectsController : ControllerBase
    {
        private static List<Project> projects = new List<Project>
        {
            new Project { Id = 1, Name = "Project 1", CreatedAt = DateTime.Now },
            new Project { Id = 2, Name = "Project 2", CreatedAt = DateTime.Now}
        };


        // GET: api/projects
        [HttpGet]
        public ActionResult<IEnumerable<Project>> GetProjects()
        {
            return Ok(projects);
        }

        // GET api/projects
        [HttpGet("{id}")]
        public ActionResult<Project> GetProject(int id)
        {
            Project project = projects.FirstOrDefault(p => p.Id == id);
            if (project == null)
            {
                return NotFound();
            }
            return Ok(project);
        }

        // POST api/projects
        [HttpPost]
        public ActionResult Post([FromBody] Project project)
        {
            project.Id = projects.Max(p => p.Id) + 1;
            projects.Add(project);
            return CreatedAtAction(nameof(GetProject), new { id = project.Id }, project);
        }

        // PUT api/projects
        [HttpPut("{id}")]
        public ActionResult Put([FromRoute] int id, [FromBody] Project updatedProject)
        {
            Project project = projects.FirstOrDefault(p => p.Id == id);
            if (project == null)
            {
                return NotFound();
            }

            project.Name = updatedProject.Name;
            project.CreatedAt = updatedProject.CreatedAt;
            return NoContent();
        }

        // DELETE api/projects
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            Project project = projects.FirstOrDefault(p => p.Id == id);
            if (project == null)
            {
                return NotFound();
            }

            projects.Remove(project);
            return NoContent();
        }
    }
}
