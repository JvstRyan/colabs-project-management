using Colabs.ProjectManagement.Application.Responses;

namespace Colabs.ProjectManagement.Application.Features.Auth.Commands.Register
{
    public class RegisterUserCommandResponse : BaseResponse
    {
        public RegisterUserCommandResponse() : base()
        {
        }
        
        public string Token { get; set; } = string.Empty;
        public string UserId { get; set; } = string.Empty;
    }
    
}
