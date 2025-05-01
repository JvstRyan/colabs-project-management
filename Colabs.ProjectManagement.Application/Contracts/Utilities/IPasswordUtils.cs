namespace Colabs.ProjectManagement.Application.Contracts.Utilities
{
    public interface IPasswordUtils
    {
        string HashPassword(string password);
        bool VerifyPassword(string password, string hashedPassword);
    }
}
