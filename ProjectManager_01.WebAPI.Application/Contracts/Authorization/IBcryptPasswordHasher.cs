namespace ProjectManager_01.Application.Contracts.Authorization;

public interface IBcryptPasswordHasher
{
    string HashPassword(string password);
    bool VerifyPassword(string hashedPassword, string providedPassword);
}
