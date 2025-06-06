using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using ProjectManager_01.Application.Contracts.Services;
using ProjectManager_01.Application.DTOs.Permissions;

namespace ProjectManager_01.Controllers;

[EnableRateLimiting("fixedlimit")]
[Route("api/[controller]")]
[ApiController]
public class PermissionsController : ControllerBase
{
    private readonly IPermissionService _permissionService;

    public PermissionsController(IPermissionService permissionService)
    {
        _permissionService = permissionService;
    }

    // GET: api/permissions
    /// <summary>
    /// Get all permissions
    /// </summary>
    /// <returns>All permissions</returns>
    [HttpGet]
    public ActionResult<IEnumerable<PermissionDto>> GetPermissions()
    {
        return Ok(_permissionService.GetAllPermissionsAsync());
    }

    // GET api/permissions/{id}
    /// <summary>
    /// Get a permission by ID
    /// </summary>
    /// <param name="id"></param>
    /// <returns>Permission by its id</returns>
    [HttpGet("{id}")]
    public ActionResult<PermissionDto> GetPermission(Guid id)
    {
        return Ok(_permissionService.GetPermissionByIdAsync(id));
    }

    // POST api/permissions
    /// <summary>
    /// Create a new permission
    /// </summary>
    /// <param name="permission"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<ActionResult> CreatePermission([FromBody] PermissionCreateDto permission)
    {
        await _permissionService.CreatePermissionAsync(permission);
        return Ok();
    }

    // PUT api/permissions
    /// <summary>
    /// Update an existing permission
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
    /// Delete a permission
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeletePermission(Guid id)
    {
        await _permissionService.DeletePermissionAsync(id);
        return Ok();
    }
}
