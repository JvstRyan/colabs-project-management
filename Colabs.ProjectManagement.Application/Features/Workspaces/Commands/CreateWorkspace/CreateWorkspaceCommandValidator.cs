using FluentValidation;

namespace Colabs.ProjectManagement.Application.Features.Workspaces.Commands.CreateWorkspace
{
    public class CreateWorkspaceCommandValidator : AbstractValidator<CreateWorkspaceCommand>
    {
        public CreateWorkspaceCommandValidator()
        {
            
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Workspace naming is required")
                .MaximumLength(50).WithMessage("Workspace name must not exceed 50 characters");
            
            RuleFor(x => x.Description)
                .MaximumLength(500).WithMessage("Description must not exceed 500 characters")
                .When(x => !string.IsNullOrEmpty(x.Description));
            
            RuleFor(x => x.ProfileUrl)
                .MaximumLength(2000).WithMessage("Profile URL must not exceed 2000 characters")
                .When(x => !string.IsNullOrEmpty(x.ProfileUrl))
                .Must(BeValidUrl).WithMessage("Invalid URL format for Profile URL")
                .When(x => !string.IsNullOrEmpty(x.ProfileUrl));
            
            RuleFor(x => x.BannerUrl)
                .MaximumLength(2000).WithMessage("Banner URL must not exceed 2000 characters")
                .When(x => !string.IsNullOrEmpty(x.BannerUrl))
                .Must(BeValidUrl).WithMessage("Invalid URL format for Banner URL")
                .When(x => !string.IsNullOrEmpty(x.BannerUrl));
                
                
        }
        
        private bool BeValidUrl(string? url)
        {
            if (string.IsNullOrEmpty(url))
                return true;
            
            return Uri.TryCreate(url, UriKind.Absolute, out _);
        }
        
    }
}
