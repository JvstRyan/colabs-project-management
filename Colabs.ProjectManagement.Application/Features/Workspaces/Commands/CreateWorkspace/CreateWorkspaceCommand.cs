using MediatR;

namespace Colabs.ProjectManagement.Application.Features.Workspaces.Commands.CreateWorkspace
{
    public class CreateWorkspaceCommand : IRequest<CreateWorkspaceCommandResponse>
    {
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; } = string.Empty;
        public string? BannerUrl { get; set; } = string.Empty;
        
    }
}
