using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using ProjectManager_01.Application.Contracts.Services;
using ProjectManager_01.Application.DTOs;
using ProjectManager_01.Application.DTOs.Projects;

namespace ProjectManager_01.Controllers;

[EnableRateLimiting("fixedlimit")]
[Route("api/[controller]")]
[ApiController]
public class ProjectsController : ControllerBase
{
    private readonly IProjectService projectService;

    public ProjectsController(IProjectService projectService)
    {
        this.projectService = projectService;
    }


    // GET: api/projects
    /// <summary>
    /// Get all projects
    /// </summary>
    /// <returns>All projects</returns>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProjectDto>>> GetProjects()
    {
        try
        {
            return Ok(await projectService.GetAllProjectsAsync());
        }
        catch (Exception ex)
        {
            return Problem(ex.Message);
        }
    }

    // GET api/projects
    /// <summary>
    /// Get a project by ID
    /// </summary>
    /// <param name="id"></param>
    /// <returns>Project by its id</returns>
    [HttpGet("{id}")]
    public async Task<ActionResult<ProjectDto>> GetProject(Guid id)
    {
        try
        {
            return Ok(await projectService.GetProjectByIdAsync(id));
        }
        catch (Exception ex)
        {
            return Problem(ex.Message);
        }
    }

    // POST api/projects
    /// <summary>
    /// Create a new project
    /// </summary>
    /// <param name="project"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<ActionResult> Post([FromBody] ProjectCreateDto project)
    {
        try
        {
            await projectService.CreateProjectAsync(project);
            return Ok();
        }
        catch (Exception ex)
        {
            return Problem(ex.Message);
        }
    }

    // PUT api/projects
    /// <summary>
    /// Update an existing project
    /// </summary>
    /// <param name="updatedProject"></param>
    /// <returns></returns>
    [HttpPut]
    public async Task<ActionResult> Put([FromBody] ProjectUpdateDto updatedProject)
    {
        try
        {
            await projectService.UpdateProjectAsync(updatedProject);
            return Ok();
        }
        catch (Exception ex)
        {
            return Problem(ex.Message);
        }
    }

    // DELETE api/projects
    /// <summary>
    /// Delete a project
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(Guid id)
    {
        try
        {
            await projectService.DeleteProjectAsync(id);
            return Ok();
        }
        catch (Exception ex)
        {
            return Problem(ex.Message);
        }
    }
}