using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using ProjectManager_01.Application.Contracts.Services;
using ProjectManager_01.Application.DTOs;
using ProjectManager_01.Application.DTOs.ProjectUserRoles;
namespace ProjectManager_01.Controllers;

[EnableRateLimiting("fixedlimit")]
[Route("api/[controller]")]
[ApiController]
public class ProjectUserRolesController : ControllerBase
{

    private readonly IProjectUserRoleService projectUserRoleService;

    public ProjectUserRolesController(IProjectUserRoleService projectUserRoleService)
    {
        this.projectUserRoleService = projectUserRoleService;
    }

    // GET api/projectmembers
    /// <summary>
    /// Get all project members
    /// </summary>
    /// <returns>All project members</returns>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProjectUserRoleDto>>> GetProjectMembers()
    {
        try
        {
            return Ok(await projectUserRoleService.GetAllProjectUserRolesAsync());
        }
        catch (Exception ex)
        {
            return Problem(ex.Message);
        }
    }

    // GET api/projectmembers/{id}
    /// <summary>
    /// Get a project member by ID
    /// </summary>
    /// <param name="id"></param>
    /// <returns>Project member by id</returns>
    [HttpGet("{id}")]
    public async Task<ActionResult<ProjectUserRoleDto>> GetProjectMember(Guid id)
    {
        try
        {
            return Ok(await projectUserRoleService.GetProjectUserRoleByIdAsync(id));
        }
        catch (Exception ex)
        {
            return Problem(ex.Message);
        }
    }

    // POST api/projectmembers
    /// <summary>
    /// Create a new project member
    /// </summary>
    /// <param name="projectMember"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<ActionResult> Post([FromBody] ProjectUserRoleCreateDto projectMember)
    {
        try
        {
            await projectUserRoleService.CreateProjectUserRoleAsync(projectMember);
            return Ok();
        }
        catch (Exception ex)
        {
            return Problem(ex.Message);
        }
    }

    // PUT api/projectmembers
    /// <summary>
    /// Update an existing project member
    /// </summary>
    /// <param name="updatedProjectMember"></param>
    /// <returns></returns>
    [HttpPut]
    public async Task<ActionResult> Put([FromBody] ProjectUserRoleUpdateDto updatedProjectMember)
    {
        try
        {
            await projectUserRoleService.UpdateProjectUserRoleAsync(updatedProjectMember);
            return Ok();
        }
        catch (Exception ex)
        {
            return Problem(ex.Message);
        }
    }

    // DELETE api/projectmembers/{id}
    /// <summary>
    /// Delete a project member
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(Guid id)
    {
        try
        {
            await projectUserRoleService.DeleteProjectUserRoleAsync(id);
            return Ok();
        }
        catch (Exception ex)
        {
            return Problem(ex.Message);
        }
    }
}
