using FluentValidation;

namespace Colabs.ProjectManagement.Application.Features.WorkspaceInvitations.Commands.UpdateWorkspaceInvitation
{
    public class UpdateWorkspaceInvitationCommandValidator : AbstractValidator<UpdateWorkspaceInvitationCommand>
    {
        public UpdateWorkspaceInvitationCommandValidator()
        {
            RuleFor(x => x.WorkspaceInvitationId)
                .NotEmpty().WithMessage("Workspace invitation id cannot be empty");
            
            RuleFor(x => x.InvitationStatus)
                .NotEmpty().WithMessage("Invitation status cannot be empty");
        }
    }
}
