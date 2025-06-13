using FluentValidation;

namespace Colabs.ProjectManagement.Application.Features.WorkspaceMembers.Queries.GetAllWorkspaceMembers
{
    public class GetAllWorkspaceMembersQueryValidator : AbstractValidator<GetAllWorkspaceMembersQuery>
    {
        public GetAllWorkspaceMembersQueryValidator()
        {
            RuleFor(x => x.WorkspaceId)
                .NotEmpty().WithMessage("Workspace id cannot be empty");
        }
    }
}
