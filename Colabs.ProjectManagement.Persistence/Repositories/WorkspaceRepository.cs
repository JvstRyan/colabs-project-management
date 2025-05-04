using Colabs.ProjectManagement.Application.Contracts.Persistence;
using Colabs.ProjectManagement.Domain.Entities.Workspaces;
using Microsoft.EntityFrameworkCore;

namespace Colabs.ProjectManagement.Persistence.Repositories
{
    public class WorkspaceRepository : BaseRepository<Workspace>, IWorkspaceRepository
    {
      

        public WorkspaceRepository(ColabsDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<IReadOnlyList<Workspace>> GetWorkspacesByUserIdAsync(string userId, CancellationToken cancellationToken = default)
        {
            var workspaces = await _dbContext.Workspaces.Where(w => w
                .OwnerId == userId || _dbContext.WorkspaceMembers.Any(wm => wm.
                UserId == userId && wm.WorkspaceId == w.WorkspaceId)
                )
                .OrderByDescending(w => w.CreatedDate)
                .ToListAsync(cancellationToken);
            
            return workspaces;
        }
       
    }
}
