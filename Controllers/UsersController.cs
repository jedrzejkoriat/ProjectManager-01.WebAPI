using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using ProjectManager_01.WebAPI.Data;

namespace ProjectManager_01.WebAPI.Controllers;

[EnableRateLimiting("fixedlimit")]
[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    private static List<User> users = new List<User>
        {
            new User { Id = Guid.NewGuid(), UserName = "admin", Email = "admin@example.com", Password = "hashed_password" },
            new User { Id = Guid.NewGuid(), UserName = "user", Email = "user@example.com", Password = "hashed_password" }
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
    public ActionResult<User> GetUser(Guid id)
    {
        var user = users.FirstOrDefault(u => u.Id == id);

        if (user == null) 
            return NotFound();

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
        user.Id = Guid.NewGuid();
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
    public ActionResult Put(Guid id, [FromBody] User updatedUser)
    {
        var user = users.FirstOrDefault(u => u.Id == id);

        if (user == null) 
            return NotFound();

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
    public ActionResult Delete(Guid id)
    {
        var user = users.FirstOrDefault(u => u.Id == id);

        if (user == null) 
            return NotFound();

        users.Remove(user);

        return NoContent();
    }
}