using Colabs.ProjectManagement.Application.Contracts.Persistence;
using FluentValidation;

namespace Colabs.ProjectManagement.Application.Features.Auth.Commands.Register
{
    public class ValidateRegistrationRequest : AbstractValidator<RegisterUserCommand>
    {
        private readonly IUserRepository _userRepository;
            
        public ValidateRegistrationRequest(IUserRepository userRepository)
        {
            _userRepository = userRepository;
            
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required")
                .EmailAddress().WithMessage("A valid email address is required")
                .MustAsync(BeUniqueEmail).WithMessage("User with this email already exists");
            
            RuleFor(x => x.Username)
                .NotEmpty().WithMessage("Username is required")
                .MinimumLength(3).WithMessage("Username must be at least 3 characters")
                .MaximumLength(50).WithMessage("Username must not exceed 50 characters")
                .MustAsync(BeUniqueUsername).WithMessage("User with this username already exists");
            
            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is required")
                .MinimumLength(8).WithMessage("Password must be at least 8 characters long")
                .Matches("[A-Z]").WithMessage("Password must contain at least one uppercase letter")
                .Matches("[a-z]").WithMessage("Password must contain at least one lowercase letter")
                .Matches("[^a-zA-Z0-9]").WithMessage("Password must contain at least one special character");
        }

        private async Task<bool> BeUniqueEmail(string email, CancellationToken cancellationToken = default)
        {
            var existingUser = await _userRepository.GetUserByEmailAsync(email, cancellationToken);
            return existingUser == null;
        }
        
        private async Task<bool> BeUniqueUsername(string username, CancellationToken cancellationToken = default)
        {
            var existingUser = await _userRepository.GetUserByUsernameAsync(username, cancellationToken);
            return existingUser == null;
        }
    }
}
