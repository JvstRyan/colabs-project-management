using Colabs.ProjectManagement.Application.Responses;

namespace Colabs.ProjectManagement.IntegrationTests.Infrastructure.Storage.Infrastructure.Auth
{
    public class AuthResponse : BaseResponse
    {
        public AuthResponse() : base()
        {
            
        }
        public string Token { get; set; } = string.Empty;
        public string UserId { get; set; } = string.Empty;
    }
}
