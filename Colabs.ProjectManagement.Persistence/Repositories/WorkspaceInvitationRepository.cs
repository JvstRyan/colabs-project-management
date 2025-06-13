using Colabs.ProjectManagement.Application.Contracts.Persistence;
using Colabs.ProjectManagement.Domain.Entities.Workspaces;
using Microsoft.EntityFrameworkCore;

namespace Colabs.ProjectManagement.Persistence.Repositories
{
    public class WorkspaceInvitationRepository : BaseRepository<WorkspaceInvitation>, IWorkspaceInvitationRepository
    {


        public WorkspaceInvitationRepository(ColabsDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<IReadOnlyList<WorkspaceInvitation>> GetWorkspaceInvitationsByUserId(string userId, CancellationToken cancellationToken = default)
        {
            var workspaceInvitations = await _dbContext.WorkspaceInvitations
                .AsNoTracking()
                .Include(x => x.Workspace)
                .Include(x => x.Inviter)
                .Where(x => x.Invitee.UserId == userId && x.Status == 0)
                .OrderByDescending(w => w.CreatedDate)
                .ToListAsync(cancellationToken);
            
            return workspaceInvitations;
        }
    }
}
