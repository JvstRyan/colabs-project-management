using Colabs.ProjectManagement.Domain.Entities;

namespace Colabs.ProjectManagement.Application.Contracts.Persistence
{
    public interface IUserRepository : IGenericRepository<User>
    {
        Task<User> GetUserByEmailAsync(string email);
        Task<User> GetUserByUsernameAsync(string username);
    }
}
