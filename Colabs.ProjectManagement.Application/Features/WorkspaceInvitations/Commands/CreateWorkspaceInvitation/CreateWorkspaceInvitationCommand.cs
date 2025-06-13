using Colabs.ProjectManagement.Domain.Enums;
using MediatR;

namespace Colabs.ProjectManagement.Application.Features.WorkspaceInvitations.Commands
{
    public class CreateWorkspaceInvitationCommand : IRequest<CreateWorkspaceInvitationCommandResponse>
    {
        public string WorkspaceId {get; set;} = string.Empty;
        public string InviteeEmail { get; set; } = string.Empty; //Email of invited user
    }
}
