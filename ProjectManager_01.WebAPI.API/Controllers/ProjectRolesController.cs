using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using ProjectManager_01.Application.Contracts.Services;
using ProjectManager_01.Application.DTOs;
using ProjectManager_01.Application.DTOs.ProjectRoles;

namespace ProjectManager_01.Controllers;

[EnableRateLimiting("fixedlimit")]
[Route("api/[controller]")]
[ApiController]
public class ProjectRolesController : ControllerBase
{

    private readonly IProjectRoleService projectRoleService;

    public ProjectRolesController(IProjectRoleService projectRoleService)
    {
        this.projectRoleService = projectRoleService;
    }

    // GET: api/roles
    /// <summary>
    /// Get all project roles
    /// </summary>
    /// <returns>All project roles</returns>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProjectRoleDto>>> GetProjectRoles()
    {
        try
        {
            return Ok(await projectRoleService.GetAllProjectRolesAsync());
        }
        catch (Exception ex)
        {
            return Problem(ex.Message);
        }
    }

    // GET: api/roles/{id}
    /// <summary>
    /// Get a project role by ID
    /// </summary>
    /// <param name="id"></param>
    /// <returns>Project role by its id</returns>
    [HttpGet("{id}")]
    public async Task<ActionResult<ProjectRoleDto>> GetProjectRole(Guid id)
    {
        try
        {
            return Ok(await projectRoleService.GetProjectRoleByIdAsync(id));
        }
        catch (Exception ex)
        {
            return Problem(ex.Message);
        }
    }

    // POST: api/roles
    /// <summary>
    /// Create a new role
    /// </summary>
    /// <param name="projectRole"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<ActionResult> Post([FromBody] ProjectRoleCreateDto projectRole)
    {
        try
        {
            await projectRoleService.CreateProjectRoleAsync(projectRole);
            return Ok();
        }
        catch (Exception ex)
        {
            return Problem(ex.Message);
        }
    }

    // PUT: api/roles
    /// <summary>
    /// Update an existing role
    /// </summary>
    /// <param name="updatedProjectRole"></param>
    /// <returns></returns>
    [HttpPut]
    public async Task<ActionResult> Put([FromBody] ProjectRoleUpdateDto updatedProjectRole)
    {
        try
        {
            await projectRoleService.UpdateProjectRoleAsync(updatedProjectRole);
            return Ok();
        }
        catch (Exception ex)
        {
            return Problem(ex.Message);
        }
    }

    // DELETE: api/roles/{id}
    /// <summary>
    /// Delete a role
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(Guid id)
    {
        try
        {
            await projectRoleService.DeleteProjectRoleAsync(id);
            return Ok();
        }
        catch (Exception ex)
        {
            return Problem(ex.Message);
        }
    }
}
