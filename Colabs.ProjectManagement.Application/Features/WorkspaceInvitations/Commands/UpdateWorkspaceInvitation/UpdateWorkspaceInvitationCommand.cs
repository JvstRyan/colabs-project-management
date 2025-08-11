using System.Text.Json.Serialization;
using Colabs.ProjectManagement.Domain.Enums;
using MediatR;

namespace Colabs.ProjectManagement.Application.Features.WorkspaceInvitation.Commands.UpdateWorkspaceInvitation
{
    public class UpdateWorkspaceInvitationCommand : IRequest<UpdateWorkspaceInvitationCommandResponse>
    {
        public string WorkspaceInvitationId {get; set;} = string.Empty;
        public InvitationStatus InvitationStatus {get; set;}
    }
}
