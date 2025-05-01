using MediatR;

namespace Colabs.ProjectManagement.Application.Features.Auth.Commands.Login
{
    public class LoginUserCommand : IRequest<LoginUserCommandResponse>
    {
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
