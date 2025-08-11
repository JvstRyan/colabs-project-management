using Colabs.ProjectManagement.Application.Responses;

namespace Colabs.ProjectManagement.Application.Features.Auth.Commands.Register
{
    public record RegisterUserCommandResponse(string Token, string UserId);
    
    
}
