using Colabs.ProjectManagement.Domain.Enums;
using MediatR;

namespace Colabs.ProjectManagement.Application.Features.WorkspaceInvitations.Commands
{
    public class CreateWorkspaceInvitationCommand : IRequest<CreateWorkspaceInvitationCommandResponse>
    {
        public string WorkspaceId {get; set;} = string.Empty;
        public string InviterId { get; set; } = string.Empty; //User sending the request
        public string InviteeId { get; set; } = string.Empty;
        public InvitationStatus Status { get; set; } = InvitationStatus.Pending;
    }
}
