using Colabs.ProjectManagement.Application.Common;
using FluentValidation;

namespace Colabs.ProjectManagement.Application.Features.Auth.Commands.UploadUserAvatar
{
    public class UploadAvatarCommandValidator : AbstractValidator<UploadUserAvatarCommand>
    {
        public UploadAvatarCommandValidator()
        {
            RuleFor(x => x.UserId)
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
}
