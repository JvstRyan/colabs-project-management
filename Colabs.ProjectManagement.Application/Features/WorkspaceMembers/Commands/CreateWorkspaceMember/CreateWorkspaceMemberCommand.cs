using MediatR;

namespace Colabs.ProjectManagement.Application.Features.WorkspaceMembers.Commands.CreateWorkspaceMember
{
    public class CreateWorkspaceMemberCommand : IRequest<CreateWorkspaceMemberCommandResponse>
    {
        public string WorkspaceInvitationId {get; set;} = string.Empty;
    }
}
