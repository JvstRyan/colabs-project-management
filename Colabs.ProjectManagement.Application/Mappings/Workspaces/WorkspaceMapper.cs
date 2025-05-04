using Colabs.ProjectManagement.Application.Features.Workspaces.Commands.CreateWorkspace;
using Colabs.ProjectManagement.Domain.Entities.Workspaces;

namespace Colabs.ProjectManagement.Application.Mappings
{
    public static class WorkspaceMapper
    {
        public static Workspace ToWorkspace
            (this CreateWorkspaceCommand command, string ownerId)
        {
            return new Workspace
            {
                WorkspaceId = Guid.NewGuid().ToString(),
                Name = command.Name,
                Description = command.Description,
                OwnerId = ownerId,
                BannerUrl = command.BannerUrl
            };
        }

        public static CreateWorkspaceCommandResponse
            ToCreateWorkspaceCommandResponse(this Workspace workspace)
        {
            return new CreateWorkspaceCommandResponse
            {
                Success = true,
                Message = "Workspace created Successfully",
                Workspace = new CreateWorkspaceDto
                {
                    WorkspaceId = workspace.WorkspaceId,
                    Name = workspace.Name
                }
            };
        }
    }
    
}
