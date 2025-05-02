using Colabs.ProjectManagement.Application.Contracts.Utilities;

namespace Colabs.ProjectManagement.Infrastructure.Hashing
{
    public class PasswordHash : IPasswordUtils
    {
        private const int WorkFactor = 12;
        
        public string HashPassword(string password)
        {
           return BCrypt.Net.BCrypt.HashPassword(password, WorkFactor);
        }

        public bool VerifyPassword(string password, string hashedPassword)
        {
           return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
        }
    }
}
