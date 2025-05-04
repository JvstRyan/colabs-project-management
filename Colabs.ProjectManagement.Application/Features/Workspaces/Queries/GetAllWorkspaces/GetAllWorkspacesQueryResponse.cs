using Colabs.ProjectManagement.Application.Responses;

namespace Colabs.ProjectManagement.Application.Features.Workspaces.Queries.GetAllWorkspaces
{
    public class GetAllWorkspacesQueryResponse : BaseResponse
    {
        public GetAllWorkspacesQueryResponse() : base()
        {
            
        }
        public List<GetAllWorkspaceDto> Workspaces { get; set; }
    }
}
