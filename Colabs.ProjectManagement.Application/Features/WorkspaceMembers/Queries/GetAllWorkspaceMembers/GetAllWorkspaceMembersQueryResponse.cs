using Colabs.ProjectManagement.Application.Responses;

namespace Colabs.ProjectManagement.Application.Features.WorkspaceMembers.Queries.GetAllWorkspaceMembers
{
    public class GetAllWorkspaceMembersQueryResponse : BaseResponse
    {
        public GetAllWorkspaceMembersQueryResponse() : base()
        {
            
        }
        public List<GetAllWorkspaceMembersDto>? WorkspaceMembers {get; set;}
    }
}
