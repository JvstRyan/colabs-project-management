using Colabs.ProjectManagement.Application.Contracts.Persistence;
using Colabs.ProjectManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Colabs.ProjectManagement.Persistence.Repositories
{
    public class RoleRepository : BaseRepository<Role>, IRoleRepository
    {

        public RoleRepository(ColabsDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<Role> GetRoleByName(string workspaceId, string roleName, CancellationToken cancellationToken = default)
        {
           return await _dbContext.Roles.FirstOrDefaultAsync(r => r.WorkspaceId == workspaceId && r.Name == roleName);
        }
    }
}
