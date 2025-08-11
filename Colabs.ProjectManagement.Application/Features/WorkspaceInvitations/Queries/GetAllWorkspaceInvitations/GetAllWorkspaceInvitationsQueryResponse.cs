using Colabs.ProjectManagement.Application.Responses;

namespace Colabs.ProjectManagement.Application.Features.WorkspaceInvitation.Queries.GetAllWorkspaceInvitations
{
  public record GetAllWorkspaceInvitationsQueryResponse(List<GetAllWorkspaceInvitationsDto>? Invitations);
}
