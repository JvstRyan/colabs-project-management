using MediatR;
using Microsoft.AspNetCore.Http;

namespace Colabs.ProjectManagement.Application.Features.Workspaces.Commands.UploadWorkspaceBanner
{
    public class UploadWorkspaceBannerCommand : IRequest<UploadWorkspaceBannerResponse>
    {
        public string WorkspaceId { get; set; } = string.Empty;
        public IFormFile File {get; set;}
    }
}
