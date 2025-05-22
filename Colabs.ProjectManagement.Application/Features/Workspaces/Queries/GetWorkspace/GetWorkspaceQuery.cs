using MediatR;

namespace Colabs.ProjectManagement.Application.Features.Workspaces.Queries.GetWorkspace
{
    public class GetWorkspaceQuery : IRequest<GetWorkspaceQueryResponse>
    {
        public string WorkspaceId { get; set; } 
    }
}
