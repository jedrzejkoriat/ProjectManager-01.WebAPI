using Microsoft.AspNetCore.Identity;

namespace ProjectManager_01.WebAPI.Data
{
    internal sealed class User
    {
        // ID
        public int Id { get; set; }
        // STRING
        public string UserName { get; init; }
        public string Email { get; init; }
        public string Password { get; init; }
    }
}
