using Colabs.ProjectManagement.Application.Features.Workspaces.Commands.CreateWorkspace;
using Colabs.ProjectManagement.Application.Features.Workspaces.Queries.GetAllWorkspaces;
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
                ProfileUrl = command.ProfileUrl,
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

        public static GetAllWorkspaceDto ToGetAllWorkspaceDto(this Workspace workspace)
        {
            return new GetAllWorkspaceDto
            {
                WorkspaceId = workspace.WorkspaceId,
                Name = workspace.Name
            };
        }

        public static List<GetAllWorkspaceDto> ToGetAllWorkspaceDtoList(this IEnumerable<Workspace> workspaces)
        {
            return workspaces.Select(w => w.ToGetAllWorkspaceDto()).ToList();
        }

        public static GetAllWorkspacesQueryResponse ToGetAllWorkspaces(this List<GetAllWorkspaceDto> workspaces)
        {
            return new GetAllWorkspacesQueryResponse
            {
                Success = true,
                Message = "Workspaces retrieved successfully",
                Workspaces = workspaces
            };
        }
    }
    
}
