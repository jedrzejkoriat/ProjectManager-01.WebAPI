using Microsoft.AspNetCore.Identity;

namespace ProjectManager_01.WebAPI.Data
{
    public sealed class User
    {
        // ID
        public int Id { get; set; }
        // STRING
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
