using Colabs.ProjectManagement.Application.Common;
using FluentValidation;

namespace Colabs.ProjectManagement.Application.Features.Workspaces.Commands.UploadWorkspaceBanner
{
    public class UploadWorkspaceBannerCommandValidator : AbstractValidator<UploadWorkspaceBannerCommand>
    {
        public UploadWorkspaceBannerCommandValidator()
        {
            RuleFor(x => x.WorkspaceId)
                .NotEmpty().WithMessage("Workspace ID is required.");
                
            RuleFor(x => x.File)
                .NotNull().WithMessage("Banner file is required");
                
            RuleFor(x => x.File)
                .Must(file => file != null && FileHelper.IsValidImage(file))
                .When(x => x.File != null)
                .WithMessage("Invalid banner file. Only JPG, PNG, GIF, and WebP files up to 5MB are allowed.");
            
        }
    }
}
