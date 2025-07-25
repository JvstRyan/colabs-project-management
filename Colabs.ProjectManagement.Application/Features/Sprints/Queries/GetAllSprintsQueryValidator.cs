using FluentValidation;

namespace Colabs.ProjectManagement.Application.Features.Sprints.Queries
{
    public class GetAllSprintsQueryValidator : AbstractValidator<GetAllSprintsQuery>
    {
        public GetAllSprintsQueryValidator()
        {
            RuleFor(x => x.WorkspaceId)
                .NotEmpty().WithMessage("Please provide a valid workspace id.");
                
        }
    }
}
