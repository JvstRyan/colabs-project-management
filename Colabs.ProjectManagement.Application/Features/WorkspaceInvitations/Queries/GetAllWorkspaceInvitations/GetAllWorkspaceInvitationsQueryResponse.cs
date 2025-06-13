using Colabs.ProjectManagement.Application.Responses;

namespace Colabs.ProjectManagement.Application.Features.WorkspaceInvitations.Queries.GetAllWorkspaceInvitations
{
    public class GetAllWorkspaceInvitationsQueryResponse : BaseResponse
    {
        public GetAllWorkspaceInvitationsQueryResponse() : base()
        {
            
        }
        public List<GetAllWorkspaceInvitationsDto>? Invitations {get; set;}
    }
}
