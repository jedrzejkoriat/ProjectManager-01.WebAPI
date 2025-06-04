using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using ProjectManager_01.Application.Contracts.Services;
using ProjectManager_01.Application.DTOs.ProjectUserRoles;
namespace ProjectManager_01.Controllers;

[EnableRateLimiting("fixedlimit")]
[Route("api/[controller]")]
[ApiController]
public class ProjectUserRolesController : ControllerBase
{
    private readonly IProjectUserRoleService _projectUserRoleService;

    public ProjectUserRolesController(IProjectUserRoleService projectUserRoleService)
    {
        _projectUserRoleService = projectUserRoleService;
    }

    // GET api/projectuserroles
    /// <summary>
    /// Get all project user roles
    /// </summary>
    /// <returns>All project user roles</returns>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProjectUserRoleDto>>> GetProjectUserRoles()
    {
        try
        {
            return Ok(await _projectUserRoleService.GetAllProjectUserRolesAsync());
        }
        catch (Exception ex)
        {
            return Problem(ex.Message);
        }
    }

    // GET api/projectuserroles/{id}
    /// <summary>
    /// Get a project user role by ID
    /// </summary>
    /// <param name="id"></param>
    /// <returns>Project user role by id</returns>
    [HttpGet("{id}")]
    public async Task<ActionResult<ProjectUserRoleDto>> GetProjectUserRole(Guid id)
    {
        try
        {
            return Ok(await _projectUserRoleService.GetProjectUserRoleByIdAsync(id));
        }
        catch (Exception ex)
        {
            return Problem(ex.Message);
        }
    }

    // POST api/projectuserroles
    /// <summary>
    /// Create a new project user role
    /// </summary>
    /// <param name="projectUserRole"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<ActionResult> CreateProjectUserRole([FromBody] ProjectUserRoleCreateDto projectUserRole)
    {
        try
        {
            await _projectUserRoleService.CreateProjectUserRoleAsync(projectUserRole);
            return Ok();
        }
        catch (Exception ex)
        {
            return Problem(ex.Message);
        }
    }

    // PUT api/projectuserrole
    /// <summary>
    /// Update an existing project user role
    /// </summary>
    /// <param name="updatedProjectUserRole"></param>
    /// <returns></returns>
    [HttpPut]
    public async Task<ActionResult> UpdateProjectUserRole([FromBody] ProjectUserRoleUpdateDto updatedProjectUserRole)
    {
        try
        {
            await _projectUserRoleService.UpdateProjectUserRoleAsync(updatedProjectUserRole);
            return Ok();
        }
        catch (Exception ex)
        {
            return Problem(ex.Message);
        }
    }

    // DELETE api/projectuserrole/{id}
    /// <summary>
    /// Delete a project user role
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteProjectUserRole(Guid id)
    {
        try
        {
            await _projectUserRoleService.DeleteProjectUserRoleAsync(id);
            return Ok();
        }
        catch (Exception ex)
        {
            return Problem(ex.Message);
        }
    }
}
