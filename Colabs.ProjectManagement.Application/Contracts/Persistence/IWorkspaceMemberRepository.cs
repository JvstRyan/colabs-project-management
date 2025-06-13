using Colabs.ProjectManagement.Domain.Entities.Workspaces;

namespace Colabs.ProjectManagement.Application.Contracts.Persistence
{
    public interface IWorkspaceMemberRepository
    {
        Task<IReadOnlyList<WorkspaceMember>> GetAllWorkspaceMembersByWorkspaceId(string workspaceId, CancellationToken cancellationToken = default);
    }
}
