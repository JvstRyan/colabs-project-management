using Colabs.ProjectManagement.Application.Responses;
using Colabs.ProjectManagement.Domain.Entities.Workspaces;

namespace Colabs.ProjectManagement.Application.Features.WorkspaceInvitation.Commands
{
    public record CreateWorkspaceInvitationCommandResponse(bool IsSuccess);
}
