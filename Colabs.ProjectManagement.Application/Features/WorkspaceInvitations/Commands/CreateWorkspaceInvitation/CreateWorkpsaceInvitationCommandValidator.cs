using Colabs.ProjectManagement.Application.Contracts.Persistence;
using FluentValidation;

namespace Colabs.ProjectManagement.Application.Features.WorkspaceInvitation.Commands
{
    public class CreateWorkspaceInvitationCommandValidator : AbstractValidator<CreateWorkspaceInvitationCommand>
    {
        private readonly IUserRepository _userRepository;
            
        public CreateWorkspaceInvitationCommandValidator(IUserRepository userRepository)
        {
            _userRepository = userRepository;
            
            RuleFor(x => x.WorkspaceId)
                .NotEmpty().WithMessage("Workspace id is required");
            
            
            RuleFor(x => x.InviteeEmail)
                .NotEmpty().WithMessage("Email is required")
                .EmailAddress().WithMessage("A valid email address is required")
                .MustAsync(MustExistEmail).WithMessage("No user with this email exists");
        }

        private async Task<bool> MustExistEmail(string email, CancellationToken cancellationToken = default)
        {
            var user = await _userRepository.GetUserByEmailAsync(email, cancellationToken);
            return user != null;
        }
    }
}
