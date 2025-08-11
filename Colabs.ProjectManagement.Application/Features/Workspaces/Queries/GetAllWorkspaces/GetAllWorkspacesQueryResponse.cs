using Colabs.ProjectManagement.Application.Responses;

namespace Colabs.ProjectManagement.Application.Features.Workspaces.Queries.GetAllWorkspaces
{
    public record GetAllWorkspacesQueryResponse(List<GetAllWorkspaceDto> Workspaces);

}
      
