using FluentValidation;

namespace Colabs.ProjectManagement.Application.Features.WorkspaceMembers.Commands.CreateWorkspaceMember
{
    public class CreateWorkspaceMemberCommandValidator : AbstractValidator<CreateWorkspaceMemberCommand>
    {
        public CreateWorkspaceMemberCommandValidator()
        {
            RuleFor(x => x.WorkspaceInvitationId)
                .NotEmpty().WithMessage("WorkspaceInvitationId is required to handle invitation");
        }
    }
}
