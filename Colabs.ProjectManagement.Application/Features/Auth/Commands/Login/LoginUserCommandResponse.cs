using Colabs.ProjectManagement.Application.Responses;

namespace Colabs.ProjectManagement.Application.Features.Auth.Commands.Login
{
    public class LoginUserCommandResponse : BaseResponse
    {
        public LoginUserCommandResponse() : base()
        {
        }
        public string Token { get; set; } = string.Empty;
        public string UserId { get; set; } = string.Empty;
    }
}
