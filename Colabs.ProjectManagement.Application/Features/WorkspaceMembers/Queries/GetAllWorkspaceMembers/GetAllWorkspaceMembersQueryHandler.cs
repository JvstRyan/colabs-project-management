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
            var response = new GetAllWorkspaceMembersQueryResponse();
            
            try
            {
                var validator = new GetAllWorkspaceMembersQueryValidator();
                var validationResult = await validator.ValidateAsync(request, cancellationToken);
                
                if (!validationResult.IsValid)
                {
                    response.Success = false;
                    response.StatusCode = 400;
                    response.ValidationErrors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                    return response;
                }
                
                var workspaceMembers = await _workspaceMemberRepository.GetAllWorkspaceMembersByWorkspaceId(request.WorkspaceId, cancellationToken);
                
                foreach (var member in workspaceMembers)
                {
                    Console.WriteLine($"User null: {member.User == null}");
                }

                if (workspaceMembers != null)
                {
                    var allWorkspaceMembers = workspaceMembers.Select(x => new GetAllWorkspaceMembersDto
                    {
                        WorkspaceMemberUsername = x.User.Username,
                        WorkspaceMemberEmail = x.User.Email,
                        WorkspaceMemberRole = x.Role.Name,
                        WorkspaceMemberAvatarUrl = x.User?.AvatarUrl
                    }).ToList();
                    
                    response.Success = true;
                    response.Message = "All workspace members queried successfully!";
                    response.WorkspaceMembers = allWorkspaceMembers;
                    return response;
                }
                
                response.Success = true;
                response.Message = "No workspace members were found";
                return response;
                
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = $"Something went wrong while trying to retrieve workspace members: {ex.Message}";
                response.StatusCode = 500;
                return response;
            }
        }
    }
}
