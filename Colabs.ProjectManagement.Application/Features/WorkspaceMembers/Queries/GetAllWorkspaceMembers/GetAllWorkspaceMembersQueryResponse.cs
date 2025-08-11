using Colabs.ProjectManagement.Application.Responses;

namespace Colabs.ProjectManagement.Application.Features.WorkspaceMembers.Queries.GetAllWorkspaceMembers
{
    public record GetAllWorkspaceMembersQueryResponse(List<GetAllWorkspaceMembersDto>? WorkspaceMembers);
    
}
