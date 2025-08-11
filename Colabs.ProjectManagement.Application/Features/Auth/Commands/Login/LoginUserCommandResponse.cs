using Colabs.ProjectManagement.Application.Responses;

namespace Colabs.ProjectManagement.Application.Features.Auth.Commands.Login
{
    public record LoginUserCommandResponse(string Token, string UserId);
   
}
