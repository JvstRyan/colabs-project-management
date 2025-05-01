using Colabs.ProjectManagement.Application.Contracts.Persistence;
using Colabs.ProjectManagement.Application.Contracts.Utilities;
using FluentValidation;

namespace Colabs.ProjectManagement.Application.Features.Auth.Commands.Login
{
    public class ValidateLoginRequest : AbstractValidator<LoginUserCommand>
    {

        public ValidateLoginRequest()
        {
            
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required")
                .EmailAddress().WithMessage("A valid email address is required");
            
            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is required")
                .MinimumLength(8)
                .Matches("[A-Z]")
                .Matches("[a-z]")
                .Matches("[^a-zA-Z0-9]");
        }

        
    }
}
