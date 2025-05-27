using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using ProjectManager_01.WebAPI.Application.DTOs;
namespace ProjectManager_01.WebAPI.Controllers;

[EnableRateLimiting("fixedlimit")]
[Route("api/[controller]")]
[ApiController]
public class ProjectUserRolesController : ControllerBase
{
    private static List<ProjectUserRoleDTO> projectMembers = new List<ProjectUserRoleDTO>
        {
            new ProjectUserRoleDTO { Id  = Guid.NewGuid(), UserId =Guid.NewGuid(), ProjectId = Guid.NewGuid(), ProjectRoleId = Guid.NewGuid() },
            new ProjectUserRoleDTO { Id = Guid.NewGuid(), UserId = Guid.NewGuid(), ProjectId = Guid.NewGuid(), ProjectRoleId = Guid.NewGuid() }
        };

    // GET api/projectmembers
    /// <summary>
    /// Get all project members
    /// </summary>
    /// <returns>All project members</returns>
    [HttpGet]
    public ActionResult<IEnumerable<ProjectUserRoleDTO>> GetProjectMembers()
    {
        return Ok(projectMembers);
    }

    // GET api/projectmembers/{id}
    /// <summary>
    /// Get a project member by ID
    /// </summary>
    /// <param name="id"></param>
    /// <returns>Project member by id</returns>
    [HttpGet("{id}")]
    public ActionResult<ProjectUserRoleDTO> GetProjectMember(Guid id)
    {
        var pm = projectMembers.FirstOrDefault(p => p.Id == id);

        if (pm == null) 
            return NotFound();

        return Ok(pm);
    }

    // POST api/projectmembers
    /// <summary>
    /// Create a new project member
    /// </summary>
    /// <param name="projectMember"></param>
    /// <returns></returns>
    [HttpPost]
    public ActionResult Post([FromBody] ProjectUserRoleDTO projectMember)
    {
        projectMember.Id = Guid.NewGuid();
        projectMembers.Add(projectMember);

        return CreatedAtAction(nameof(GetProjectMember), new { id = projectMember.Id }, projectMember);
    }

    // PUT api/projectmembers/{id}
    /// <summary>
    /// Update an existing project member
    /// </summary>
    /// <param name="id"></param>
    /// <param name="updatedProjectMember"></param>
    /// <returns></returns>
    [HttpPut("{id}")]
    public ActionResult Put(Guid id, [FromBody] ProjectUserRoleDTO updatedProjectMember)
    {
        var pm = projectMembers.FirstOrDefault(p => p.Id == id);

        if (pm == null) 
            return NotFound();

        pm.UserId = updatedProjectMember.UserId;
        pm.ProjectId = updatedProjectMember.ProjectId;
        pm.ProjectRoleId = updatedProjectMember.ProjectRoleId;

        return NoContent();
    }

    // DELETE api/projectmembers/{id}
    /// <summary>
    /// Delete a project member
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete("{id}")]
    public ActionResult Delete(Guid id)
    {
        var pm = projectMembers.FirstOrDefault(p => p.Id == id);

        if (pm == null) 
            return NotFound();

        projectMembers.Remove(pm);

        return NoContent();
    }
}
