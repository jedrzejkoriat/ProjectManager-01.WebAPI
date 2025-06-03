using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using ProjectManager_01.Application.Contracts.Services;
using ProjectManager_01.Application.DTOs;
using ProjectManager_01.Application.DTOs.Permissions;

namespace ProjectManager_01.Controllers;

[EnableRateLimiting("fixedlimit")]
[Route("api/[controller]")]
[ApiController]
public class PermissionsController : ControllerBase
{

    private readonly IPermissionService permissionService;

    public PermissionsController(IPermissionService permissionService)
    {
        this.permissionService = permissionService;
    }

    // GET: api/permissions
    /// <summary>
    /// Get all permissions
    /// </summary>
    /// <returns>All permissions</returns>
    [HttpGet]
    public ActionResult<IEnumerable<PermissionDto>> GetPermissions()
    {
        try
        {
            return Ok(permissionService.GetAllPermissionsAsync());
        }
        catch (Exception ex)
        {
            return Problem(ex.Message);
        }
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
        try
        {
            return Ok(permissionService.GetPermissionByIdAsync(id));
        }
        catch (Exception ex)
        {
            return Problem(ex.Message);
        }
    }

    // POST api/permissions
    /// <summary>
    /// Create a new permission
    /// </summary>
    /// <param name="permission"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<ActionResult> Post([FromBody] PermissionCreateDto permission)
    {
        try
        {
            await permissionService.CreatePermissionAsync(permission);
            return Ok();
        }
        catch (Exception ex)
        {
            return Problem(ex.Message);
        }
    }

    // PUT api/permissions
    /// <summary>
    /// Update an existing permission
    /// </summary>
    /// <param name="updatedPermission"></param>
    /// <returns></returns>
    [HttpPut]
    public async Task<ActionResult> Put([FromBody] PermissionUpdateDto updatedPermission)
    {
        try
        {
            await permissionService.UpdatePermissionAsync(updatedPermission);
            return Ok();
        }
        catch (Exception ex)
        {
            return Problem(ex.Message);
        }
    }

    // DELETE api/permissions/{id}
    /// <summary>
    /// Delete a permission
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(Guid id)
    {
        try
        {
            await permissionService.DeletePermissionAsync(id);
            return Ok();
        }
        catch (Exception ex)
        {
            return Problem(ex.Message);
        }
    }
}
