using Colabs.ProjectManagement.Domain.Entities;
using Colabs.ProjectManagement.Domain.Entities.Workspaces;
using Colabs.ProjectManagement.Domain.Enums;

namespace Colabs.ProjectManagement.Application.Features.WorkspaceInvitation.Queries.GetAllWorkspaceInvitations
{
    public class GetAllWorkspaceInvitationsDto
    {
        public string WorkspaceInvitationId { get; set; } = string.Empty;
        public string InviterUsername { get; set; } = string.Empty;
        public string WorkspaceName { get; set; } = string.Empty;
        public string? WorkspaceProfileUrl { get; set; }
    }
}
