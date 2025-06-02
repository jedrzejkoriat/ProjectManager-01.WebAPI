using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using ProjectManager_01.Application.Contracts.Services;
using ProjectManager_01.Application.DTOs;
using ProjectManager_01.Application.DTOs.Roles;

namespace ProjectManager_01.Controllers;

[EnableRateLimiting("fixedlimit")]
[Route("api/[controller]")]
[ApiController]
public class RolesController : ControllerBase
{
    private readonly IRoleService roleService;

    public RolesController(IRoleService roleService)
    {
        this.roleService = roleService;
    }

    // GET: api/roles
    /// <summary>
    /// Get all roles
    /// </summary>
    /// <returns>All roles</returns>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<RoleDto>>> GetRoles()
    {
        try
        {
            return Ok(await roleService.GetAllRolesAsync());
        }
        catch (Exception ex)
        {
            return Problem(ex.Message);
        }
    }

    // GET api/roles/{id}
    /// <summary>
    /// Get a role by ID
    /// </summary>
    /// <param name="id"></param>
    /// <returns>Role by its id</returns>
    [HttpGet("{id}")]
    public async Task<ActionResult<RoleDto>> GetRole(Guid id)
    {
        try
        {
            return Ok(await roleService.GetRoleByIdAsync(id));
        }
        catch (Exception ex)
        {
            return Problem(ex.Message);
        }
    }

    // POST api/roles
    /// <summary>
    /// Create a new role
    /// </summary>
    /// <param name="role"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<ActionResult> Post([FromBody] RoleCreateDto role)
    {
        try
        {
            await roleService.CreateRoleAsync(role);
            return Ok();
        }
        catch (Exception ex)
        {
            return Problem(ex.Message);
        }
    }

    // PUT api/roles
    /// <summary>
    /// Update an existing role
    /// </summary>
    /// <param name="updatedRole"></param>
    /// <returns></returns>
    [HttpPut]
    public async Task<ActionResult> Put([FromBody] RoleUpdateDto updatedRole)
    {
        try
        {
            await roleService.UpdateRoleAsync(updatedRole);
            return Ok();
        }
        catch (Exception ex)
        {
            return Problem(ex.Message);
        }
    }

    // DELETE api/roles/{id}
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
            await roleService.DeleteRoleAsync(id);
            return Ok();
        }
        catch (Exception ex)
        {
            return Problem(ex.Message);
        }
    }
}
