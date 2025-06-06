using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using ProjectManager_01.Application.Constants;
using ProjectManager_01.Application.Contracts.Services;
using ProjectManager_01.Application.DTOs.Roles;

namespace ProjectManager_01.Controllers;

[EnableRateLimiting("fixedlimit")]
[Route("api/[controller]")]
[ApiController]
[Authorize(Roles = Roles.Admin)]
public class RolesController : ControllerBase
{
    private readonly IRoleService _roleService;

    public RolesController(IRoleService roleService)
    {
        _roleService = roleService;
    }

    // GET: api/roles
    /// <summary>
    /// Get all roles
    /// </summary>
    /// <returns>All roles</returns>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<RoleDto>>> GetRoles()
    {
        return Ok(await _roleService.GetAllRolesAsync());
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
        return Ok(await _roleService.GetRoleByIdAsync(id));
    }

    // POST api/roles
    /// <summary>
    /// Create a new role
    /// </summary>
    /// <param name="role"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<ActionResult> CreateRole([FromBody] RoleCreateDto role)
    {
        await _roleService.CreateRoleAsync(role);
        return Ok();
    }

    // PUT api/roles
    /// <summary>
    /// Update an existing role
    /// </summary>
    /// <param name="updatedRole"></param>
    /// <returns></returns>
    [HttpPut]
    public async Task<ActionResult> UpdateRole([FromBody] RoleUpdateDto updatedRole)
    {
        await _roleService.UpdateRoleAsync(updatedRole);
        return Ok();
    }

    // DELETE api/roles/{id}
    /// <summary>
    /// Delete a role
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteRole(Guid id)
    {
        await _roleService.DeleteRoleAsync(id);
        return Ok();
    }
}
