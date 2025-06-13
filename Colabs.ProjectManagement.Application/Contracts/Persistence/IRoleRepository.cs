using Colabs.ProjectManagement.Domain.Entities;

namespace Colabs.ProjectManagement.Application.Contracts.Persistence
{
    public interface IRoleRepository : IGenericRepository<Role>
    {
        Task<Role> GetRoleByName(string workspaceId, string roleName, CancellationToken cancellationToken = default);
    }
}
