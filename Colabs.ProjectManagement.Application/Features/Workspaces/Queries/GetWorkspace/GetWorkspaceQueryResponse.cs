using Colabs.ProjectManagement.Application.Responses;

namespace Colabs.ProjectManagement.Application.Features.Workspaces.Queries.GetWorkspace
{
    public class GetWorkspaceQueryResponse : BaseResponse
    {
        public GetWorkspaceQueryResponse(): base()
        {
            
        }
        
        public GetWorkspaceQueryDto Workspace {get; set;}
    }
}
