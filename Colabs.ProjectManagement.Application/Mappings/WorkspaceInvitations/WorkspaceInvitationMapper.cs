using Colabs.ProjectManagement.Application.Features.WorkspaceInvitation.Commands;
using Colabs.ProjectManagement.Domain.Entities.Workspaces;
using Colabs.ProjectManagement.Domain.Enums;

namespace Colabs.ProjectManagement.Application.Mappings.WorkspaceInvitations
{
    public static class WorkspaceInvitationMapper
    {
        public static WorkspaceInvitation ToWorkspaceInvitation(this CreateWorkspaceInvitationCommand command,
            string inviterId, string inviteeId)
        {
            return new WorkspaceInvitation
            {
                WorkspaceInvitationId = Guid.NewGuid().ToString(),
                WorkspaceId = command.WorkspaceId,
                InviterId = inviterId,
                InviteeId = inviteeId,
                Status = InvitationStatus.Pending
            };
        }
    }
}
