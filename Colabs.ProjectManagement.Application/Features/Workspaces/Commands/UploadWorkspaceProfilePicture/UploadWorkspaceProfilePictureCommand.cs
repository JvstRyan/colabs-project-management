using MediatR;
using Microsoft.AspNetCore.Http;

namespace Colabs.ProjectManagement.Application.Features.Workspaces.Commands.UploadWorkpsaceProfilePicture
{
    public class UploadWorkspaceProfilePictureCommand : IRequest<UploadWorkspaceProfilePictureResponse>
    {
        public string WorkspaceId { get; set; } = string.Empty;
        public IFormFile File { get; set; }
    }
}
