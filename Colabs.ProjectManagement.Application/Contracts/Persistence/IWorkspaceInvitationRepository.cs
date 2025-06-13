using Colabs.ProjectManagement.Domain.Entities.Workspaces;

namespace Colabs.ProjectManagement.Application.Contracts.Persistence
{
    public interface IWorkspaceInvitationRepository : IGenericRepository<WorkspaceInvitation>
    {
        Task<IReadOnlyList<WorkspaceInvitation>> GetWorkspaceInvitationsByUserId(string userId, CancellationToken cancellationToken = default);
    }
}
