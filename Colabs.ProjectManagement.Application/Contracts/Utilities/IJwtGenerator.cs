using Colabs.ProjectManagement.Domain.Entities;

namespace Colabs.ProjectManagement.Application.Contracts.Utilities
{
    public interface IJwtGenerator
    {
        string CreateToken(User user);
    }
}
