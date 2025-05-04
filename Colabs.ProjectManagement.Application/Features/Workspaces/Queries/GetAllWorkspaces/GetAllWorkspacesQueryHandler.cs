using Colabs.ProjectManagement.Application.Contracts;
using Colabs.ProjectManagement.Application.Contracts.Persistence;
using Colabs.ProjectManagement.Application.Mappings;
using MediatR;

namespace Colabs.ProjectManagement.Application.Features.Workspaces.Queries.GetAllWorkspaces
{
    public class GetAllWorkspacesQueryHandler : IRequestHandler<GetAllWorkspacesQuery, GetAllWorkspacesQueryResponse>
    {
        private readonly ICurrentLoggedInUserService _currentLoggedInUser;
        private readonly IWorkspaceRepository _workspaceRepository;

        public GetAllWorkspacesQueryHandler(ICurrentLoggedInUserService currentLoggedInUser, IWorkspaceRepository
            workspaceRepository)
        {
            _workspaceRepository = workspaceRepository;
            _currentLoggedInUser = currentLoggedInUser;
        }

        public async Task<GetAllWorkspacesQueryResponse> Handle(GetAllWorkspacesQuery request, CancellationToken cancellationToken)
        {
            var userId = _currentLoggedInUser.UserId;
            Console.WriteLine($"This is the logged user id {userId}"); 
            var workspaces = await _workspaceRepository.GetWorkspacesByUserIdAsync(userId, cancellationToken);

            if (workspaces == null || !workspaces.Any())
            {
                return new GetAllWorkspacesQueryResponse
                {
                    Success = true,
                    Message = "No workspaces found for the current user",
                    Workspaces = new List<GetAllWorkspaceDto>()
                };
            }
            
            var workspaceDtos = workspaces.ToGetAllWorkspaceDtoList();
            return workspaceDtos.ToGetAllWorkspaces();
        }
    }
}
