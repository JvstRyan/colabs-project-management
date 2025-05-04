using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Colabs.ProjectManagement.Application.Contracts;

namespace Colabs.ProjectManagement.Api.Services
{
    public class CurrentLoggedInUserService : ICurrentLoggedInUserService
    {
        private readonly IHttpContextAccessor _contextAccessor;

        public CurrentLoggedInUserService(IHttpContextAccessor httpContextAccessor)
        {
            _contextAccessor = httpContextAccessor;
        }

        public string UserId
        {
            get
            {
                return _contextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? string.Empty;
            }
        }
    }
}
