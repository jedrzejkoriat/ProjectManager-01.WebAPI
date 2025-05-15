using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectManager_01.WebAPI.Data;

namespace ProjectManager_01.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectMembersController : ControllerBase
    {
        private static List<ProjectMember> projectMembers = new List<ProjectMember>
        {
            new ProjectMember { Id = 1, UserId = 1, ProjectId = 1, RoleId = 1, JoinDate = DateTime.Now },
            new ProjectMember { Id = 2, UserId = 2, ProjectId = 1, RoleId = 2, JoinDate = DateTime.Now }
        };

        [HttpGet]
        public ActionResult<IEnumerable<ProjectMember>> GetProjectMembers() => Ok(projectMembers);

        [HttpGet("{id}")]
        public ActionResult<ProjectMember> GetProjectMember(int id)
        {
            var pm = projectMembers.FirstOrDefault(p => p.Id == id);
            if (pm == null) return NotFound();
            return Ok(pm);
        }

        [HttpPost]
        public ActionResult Post([FromBody] ProjectMember projectMember)
        {
            projectMember.Id = projectMembers.Max(p => p.Id) + 1;
            projectMembers.Add(projectMember);
            return CreatedAtAction(nameof(GetProjectMember), new { id = projectMember.Id }, projectMember);
        }

        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] ProjectMember updatedProjectMember)
        {
            var pm = projectMembers.FirstOrDefault(p => p.Id == id);
            if (pm == null) return NotFound();

            pm.UserId = updatedProjectMember.UserId;
            pm.ProjectId = updatedProjectMember.ProjectId;
            pm.RoleId = updatedProjectMember.RoleId;
            pm.JoinDate = updatedProjectMember.JoinDate;
            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var pm = projectMembers.FirstOrDefault(p => p.Id == id);
            if (pm == null) return NotFound();

            projectMembers.Remove(pm);
            return NoContent();
        }
    }
}
