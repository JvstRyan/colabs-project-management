namespace Colabs.ProjectManagement.Application.Contracts.Utilities
{
    public interface IPasswordHasher
    {
        string HashPassword(string password);
        bool VerifyPassword(string password, string hashedPassword);
    }
}
