using Colabs.ProjectManagement.Application.Contracts.Persistence;
using MediatR;

namespace Colabs.ProjectManagement.Application.Features.WorkspaceMembers.Queries.GetAllWorkspaceMembers
{
    public class GetAllWorkspaceMembersQueryHandler : IRequestHandler<GetAllWorkspaceMembersQuery, GetAllWorkspaceMembersQueryResponse>
    {
        private readonly IWorkspaceMemberRepository _workspaceMemberRepository;

        public GetAllWorkspaceMembersQueryHandler(IWorkspaceMemberRepository workspaceMemberRepository)
        {
            _workspaceMemberRepository = workspaceMemberRepository;
        }

        public async Task<GetAllWorkspaceMembersQueryResponse> Handle(GetAllWorkspaceMembersQuery request, CancellationToken cancellationToken)
        {
  
           var workspaceMembers = await _workspaceMemberRepository.GetAllWorkspaceMembersByWorkspaceId(request.WorkspaceId, cancellationToken);

            if (workspaceMembers is null)
                return new GetAllWorkspaceMembersQueryResponse(new List<GetAllWorkspaceMembersDto>());

            var allWorkspaceMembers = workspaceMembers.Select(x => new GetAllWorkspaceMembersDto
            {
                WorkspaceMemberUsername = x.User.Username,
                WorkspaceMemberEmail = x.User.Email,
                WorkspaceMemberRole = x.Role.Name,
                WorkspaceMemberAvatarUrl = x.User?.AvatarUrl
            }).ToList();

           return new GetAllWorkspaceMembersQueryResponse(allWorkspaceMembers);
               
        }
    }
}
