using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using ProjectManager_01.Application.Constants;
using ProjectManager_01.Application.Contracts.Services;
using ProjectManager_01.Application.DTOs.Permissions;

namespace ProjectManager_01.Controllers;

/// <summary>
/// Controller for managing Permissions (readonly) - Admin authorization.
/// </summary>
[EnableRateLimiting("fixedlimit")]
[Route("api/[controller]")]
[ApiController]
[Authorize(Roles = Roles.Admin)]
public class PermissionsController : ControllerBase
{
    private readonly IPermissionService _permissionService;

    public PermissionsController(IPermissionService permissionService)
    {
        _permissionService = permissionService;
    }

    // GET: api/permissions
    /// <summary>
    /// Get all Permissions - Admin only
    /// </summary>
    /// <returns>All Permissions</returns>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<PermissionDto>>> GetPermissions()
    {
        return Ok(await _permissionService.GetAllPermissionsAsync());
    }

    // GET api/permissions/{id}
    /// <summary>
    /// Get Permission by Id - Admin only
    /// </summary>
    /// <param name="id"></param>
    /// <returns>Permission by Id</returns>
    [HttpGet("{id}")]
    public async Task<ActionResult<PermissionDto>> GetPermission(Guid id)
    {
        return Ok(await _permissionService.GetPermissionByIdAsync(id));
    }

    // POST api/permissions
    /// <summary>
    /// Create Permission - Admin only (INSERT is denied on db side)
    /// </summary>
    /// <param name="permission"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<ActionResult> CreatePermission([FromBody] PermissionCreateDto permission)
    {
        await  _permissionService.CreatePermissionAsync(permission);
        return Ok();
    }

    // PUT api/permissions
    /// <summary>
    /// Update Permission - Admin only (UPDATE is denied on db side)
    /// </summary>
    /// <param name="updatedPermission"></param>
    /// <returns></returns>
    [HttpPut]
    public async Task<ActionResult> UpdatePermission([FromBody] PermissionUpdateDto updatedPermission)
    {
        await _permissionService.UpdatePermissionAsync(updatedPermission);
        return Ok();
    }

    // DELETE api/permissions/{id}
    /// <summary>
    /// Delete Permission by Id - Admin only (DELETE is denied on db side)
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeletePermission(Guid id)
    {
        await  _permissionService.DeletePermissionAsync(id);
        return Ok();
    }
}
