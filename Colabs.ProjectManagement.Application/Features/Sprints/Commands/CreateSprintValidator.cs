using FluentValidation;

namespace Colabs.ProjectManagement.Application.Features.Sprints.Commands
{
    public class CreateSprintValidator : AbstractValidator<CreateSprintCommand>
    {
        public CreateSprintValidator()
        {
            
            RuleFor(x => x.WorkspaceId)
                .NotEmpty().WithMessage("{PropertyName} is required");
            
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("{PropertyName} is required!")
                .MaximumLength(50).WithMessage("{PropertyName} must not exceed 50 characters");
            
            
            When(x => x.StartDate.HasValue, () =>
            {
                RuleFor(x => x.StartDate)
                    .Must(date => date >= DateTime.Today)
                    .WithMessage("Start date cannot be in the past");
            });
            
            When(p => p.EndDate.HasValue, () => {
                RuleFor(p => p.EndDate)
                    .Must(date => date >= DateTime.Today)
                    .WithMessage("End date cannot be in the past.");
            });
            
           
            When(p => p.StartDate.HasValue && p.EndDate.HasValue, () => {
                RuleFor(p => p.EndDate)
                    .GreaterThanOrEqualTo(p => p.StartDate)
                    .WithMessage("End date must be on or after the start date.");
            });

           
            When(p => p.StartDate.HasValue, () => {
                RuleFor(p => p.EndDate)
                    .NotNull()
                    .WithMessage("End date must be provided when start date is specified.");
            });
            
            When(p => p.EndDate.HasValue, () => {
                RuleFor(p => p.StartDate)
                    .NotNull()
                    .WithMessage("Start date must be provided when end date is specified.");
            });


            
            RuleFor(x => x.Description)
                .MaximumLength(500).WithMessage("{PropertyName} must not exceed 500 characters");
            
        }
    }
}
