using MediatR;

namespace Colabs.ProjectManagement.Application.Features.WorkspaceMembers.Queries.GetAllWorkspaceMembers
{
    public class GetAllWorkspaceMembersQuery : IRequest<GetAllWorkspaceMembersQueryResponse>
    {
        public string WorkspaceId { get; set; }
    }
}
