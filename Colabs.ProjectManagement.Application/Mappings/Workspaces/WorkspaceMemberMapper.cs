using Colabs.ProjectManagement.Domain.Entities;
using Colabs.ProjectManagement.Domain.Entities.Workspaces;

namespace Colabs.ProjectManagement.Application.Mappings
{
    public static class WorkspaceMemberMapper
    {
        public static WorkspaceMember ToWorkspaceMember(this Workspace workspace, Role role)
        {
            return new WorkspaceMember
            {
                WorkspaceId = workspace.WorkspaceId,
                UserId = workspace.OwnerId,
                RoleId = role.RoleId, 
                JoinedAt = DateTime.UtcNow
            };
        }
    }
}
