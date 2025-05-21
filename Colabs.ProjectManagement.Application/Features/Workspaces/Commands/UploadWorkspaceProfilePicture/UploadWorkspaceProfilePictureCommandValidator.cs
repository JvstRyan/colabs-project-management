using Colabs.ProjectManagement.Application.Common;
using Colabs.ProjectManagement.Application.Features.Workspaces.Commands.UploadWorkpsaceProfilePicture;
using FluentValidation;

namespace Colabs.ProjectManagement.Application.Features.Workspaces.Commands.UploadWorkpspaceProfilePicture
{
    public class UploadWorkspaceProfilePictureCommandValidator : AbstractValidator<UploadWorkspaceProfilePictureCommand>
    {
        public UploadWorkspaceProfilePictureCommandValidator()
        {
            RuleFor(x => x.WorkspaceId)
                .NotEmpty().WithMessage("Workspace ID is required.");
                
            RuleFor(x => x.File)
                .NotNull().WithMessage("Profile picture file is required.");
                
            RuleFor(x => x.File)
                .Must(file => file != null && FileHelper.IsValidImage(file))
                .When(x => x.File != null)
                .WithMessage("Invalid image file. Only JPG, PNG, GIF, and WebP files up to 5MB are allowed.");
        }

    }
}
