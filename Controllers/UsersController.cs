using Microsoft.AspNetCore.Mvc;
using ProjectManager_01.WebAPI.Data;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ProjectManager_01.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private static List<User> users = new List<User>
        {
            new User { Id = 1, UserName = "admin", Email = "admin@example.com", Password = "hashed_password" },
            new User { Id = 2, UserName = "user", Email = "user@example.com", Password = "hashed_password" }
        };

        // GET: api/users
        /// <summary>
        /// Get all users
        /// </summary>
        /// <returns>All urses</returns>
        [HttpGet]
        public ActionResult<IEnumerable<User>> GetUsers()
        {
            return Ok(users);
        }

        // GET: api/users/{id}
        /// <summary>
        /// Get a user by ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns>User by its id</returns>
        [HttpGet("{id}")]
        public ActionResult<User> GetUser(int id)
        {
            var user = users.FirstOrDefault(u => u.Id == id);
            if (user == null) return NotFound();
            return Ok(user);
        }

        // POST: api/users
        /// <summary>
        /// Create a new user
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Post([FromBody] User user)
        {
            user.Id = users.Max(u => u.Id) + 1;
            users.Add(user);
            return CreatedAtAction(nameof(GetUser), new { id = user.Id }, user);
        }

        // PUT: api/users/{id}
        /// <summary>
        /// Update an existing user
        /// </summary>
        /// <param name="id"></param>
        /// <param name="updatedUser"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] User updatedUser)
        {
            var user = users.FirstOrDefault(u => u.Id == id);
            if (user == null) return NotFound();

            user.UserName = updatedUser.UserName;
            user.Email = updatedUser.Email;
            user.Password = updatedUser.Password;
            return NoContent();
        }

        // DELETE: api/users/{id}
        /// <summary>
        /// Delete a user
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var user = users.FirstOrDefault(u => u.Id == id);
            if (user == null) return NotFound();

            users.Remove(user);
            return NoContent();
        }
    }
}
