using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectManager_01.WebAPI.Data;

namespace ProjectManager_01.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private static List<Role> roles = new List<Role>
        {
            new Role { Id = 1, Name = "Admin" },
            new Role { Id = 2, Name = "User" }
        };

        [HttpGet]
        public ActionResult<IEnumerable<Role>> GetRoles() => Ok(roles);

        [HttpGet("{id}")]
        public ActionResult<Role> GetRole(int id)
        {
            var role = roles.FirstOrDefault(r => r.Id == id);
            if (role == null) return NotFound();
            return Ok(role);
        }

        [HttpPost]
        public ActionResult Post([FromBody] Role role)
        {
            role.Id = roles.Max(r => r.Id) + 1;
            roles.Add(role);
            return CreatedAtAction(nameof(GetRole), new { id = role.Id }, role);
        }

        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] Role updatedRole)
        {
            var role = roles.FirstOrDefault(r => r.Id == id);
            if (role == null) return NotFound();

            role.Name = updatedRole.Name;
            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var role = roles.FirstOrDefault(r => r.Id == id);
            if (role == null) return NotFound();

            roles.Remove(role);
            return NoContent();
        }
    }
}
