using MediatR;
using Microsoft.AspNetCore.Http;

namespace Colabs.ProjectManagement.Application.Features.Auth.Commands.UploadUserAvatar
{
    public class UploadUserAvatarCommand : IRequest<UploadUserAvatarCommandResponse>
    {
        public string UserId { get; set; } = string.Empty;
        public IFormFile File { get; set; }
    }
}
