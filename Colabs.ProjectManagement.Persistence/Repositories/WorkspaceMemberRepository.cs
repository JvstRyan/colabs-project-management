using Colabs.ProjectManagement.Application.Contracts.Persistence;
using Colabs.ProjectManagement.Domain.Entities.Workspaces;
using Microsoft.EntityFrameworkCore;

namespace Colabs.ProjectManagement.Persistence.Repositories
{
    public class WorkspaceMemberRepository : BaseRepository<WorkspaceMember>, IWorkspaceMemberRepository
    {

        public WorkspaceMemberRepository(ColabsDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<IReadOnlyList<WorkspaceMember>> GetAllWorkspaceMembersByWorkspaceId(string workspaceId, CancellationToken cancellationToken = default)
        {
            var workspaceMembers = await _dbContext.WorkspaceMembers
                .AsNoTracking()
                .Include(x => x.User)
                .Include(x => x.Role)
                .Where(x => x.WorkspaceId == workspaceId)
                .ToListAsync(cancellationToken);
            
            return workspaceMembers;
        }
    }
}
