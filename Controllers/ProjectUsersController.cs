using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using ProjectManager_01.WebAPI.Data;

namespace ProjectManager_01.WebAPI.Controllers;

[EnableRateLimiting("fixedlimit")]
[Route("api/[controller]")]
[ApiController]
public class ProjectUsersController : ControllerBase
{
    private static List<ProjectUser> projectMembers = new List<ProjectUser>
        {
            new ProjectUser { Id  = Guid.NewGuid(), UserId =Guid.NewGuid(), ProjectId = Guid.NewGuid(), ProjectRoleId = Guid.NewGuid(), JoinDate = DateTime.Now },
            new ProjectUser { Id = Guid.NewGuid(), UserId = Guid.NewGuid(), ProjectId = Guid.NewGuid(), ProjectRoleId = Guid.NewGuid(), JoinDate = DateTime.Now }
        };

    // GET api/projectmembers
    /// <summary>
    /// Get all project members
    /// </summary>
    /// <returns>All project members</returns>
    [HttpGet]
    public ActionResult<IEnumerable<ProjectUser>> GetProjectMembers()
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
    public ActionResult<ProjectUser> GetProjectMember(Guid id)
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
    public ActionResult Post([FromBody] ProjectUser projectMember)
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
    public ActionResult Put(Guid id, [FromBody] ProjectUser updatedProjectMember)
    {
        var pm = projectMembers.FirstOrDefault(p => p.Id == id);

        if (pm == null) 
            return NotFound();

        pm.UserId = updatedProjectMember.UserId;
        pm.ProjectId = updatedProjectMember.ProjectId;
        pm.ProjectRoleId = updatedProjectMember.ProjectRoleId;
        pm.JoinDate = updatedProjectMember.JoinDate;

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
