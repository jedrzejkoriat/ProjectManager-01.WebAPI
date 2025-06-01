using ProjectManager_01.Application.Contracts.Authorization;

namespace ProjectManager_01.Infrastructure.Authorization;
internal class BcryptPasswordHasher : IBcryptPasswordHasher
{
    public string HashPassword(string password)
    {
        return BCrypt.Net.BCrypt.HashPassword(password);
    }

    public bool VerifyPassword(string hashedPassword, string providedPassword)
    {
        return BCrypt.Net.BCrypt.Verify(providedPassword, hashedPassword);
    }
}
