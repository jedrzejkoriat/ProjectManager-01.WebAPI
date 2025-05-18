using Microsoft.AspNetCore.Mvc;
using ProjectManager_01.WebAPI.Data;

namespace ProjectManager_01.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProjectsController : ControllerBase
{
    private static List<Project> projects = new List<Project>
        {
            new Project { Id = Guid.NewGuid(), Name = "Project 1", CreatedAt = DateTime.Now },
            new Project { Id = Guid.NewGuid(), Name = "Project 2", CreatedAt = DateTime.Now}
        };

    // GET: api/projects
    /// <summary>
    /// Get all projects
    /// </summary>
    /// <returns>All projects</returns>
    [HttpGet]
    public ActionResult<IEnumerable<Project>> GetProjects()
    {
        return Ok(projects);
    }

    // GET api/projects
    /// <summary>
    /// Get a project by ID
    /// </summary>
    /// <param name="id"></param>
    /// <returns>Project by its id</returns>
    [HttpGet("{id}")]
    public ActionResult<Project> GetProject(Guid id)
    {
        Project project = projects.FirstOrDefault(p => p.Id == id);

        if (project == null) 
            return NotFound();

        return Ok(project);
    }

    // POST api/projects
    /// <summary>
    /// Create a new project
    /// </summary>
    /// <param name="project"></param>
    /// <returns></returns>
    [HttpPost]
    public ActionResult Post([FromBody] Project project)
    {
        project.Id = Guid.NewGuid();
        projects.Add(project);

        return CreatedAtAction(nameof(GetProject), new { id = project.Id }, project);
    }

    // PUT api/projects
    /// <summary>
    /// Update an existing project
    /// </summary>
    /// <param name="id"></param>
    /// <param name="updatedProject"></param>
    /// <returns></returns>
    [HttpPut("{id}")]
    public ActionResult Put(Guid id, [FromBody] Project updatedProject)
    {
        Project project = projects.FirstOrDefault(p => p.Id == id);

        if (project == null) 
            return NotFound();

        project.Name = updatedProject.Name;
        project.CreatedAt = updatedProject.CreatedAt;

        return NoContent();
    }

    // DELETE api/projects
    /// <summary>
    /// Delete a project
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete("{id}")]
    public ActionResult Delete(Guid id)
    {
        Project project = projects.FirstOrDefault(p => p.Id == id);

        if (project == null) 
            return NotFound();

        projects.Remove(project);

        return NoContent();
    }
}