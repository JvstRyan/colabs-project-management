using Colabs.ProjectManagement.Application.Contracts;
using Colabs.ProjectManagement.Application.Contracts.Persistence;
using Colabs.ProjectManagement.Application.Exceptions;
using Colabs.ProjectManagement.Application.Mappings.Sprints;
using Colabs.ProjectManagement.Domain.Entities.TaskManagement;
using MediatR;

namespace Colabs.ProjectManagement.Application.Features.Sprints.Commands
{
    public class CreateSprintCommandHandler : IRequestHandler<CreateSprintCommand, CreateSprintCommandResult>
    {
        private readonly IGenericRepository<Sprint> _sprintRepository;
        private readonly IWorkspaceRepository _workspaceRepository;
        private readonly ICurrentLoggedInUserService _userService;

        public CreateSprintCommandHandler(IGenericRepository<Sprint> sprintRepository, IWorkspaceRepository workspaceRepository,
            ICurrentLoggedInUserService userService)
        {
            _sprintRepository = sprintRepository;
            _workspaceRepository = workspaceRepository;
            _userService = userService;
        }

        public async Task<CreateSprintCommandResult> Handle(CreateSprintCommand request, CancellationToken cancellationToken)
        {
            
            // verifying if the workspace exists
            var workspace = await _workspaceRepository.GetWorkspaceMembersAsync(request.WorkspaceId, cancellationToken);
            
            if (workspace is null)
                throw new NotFoundException("Workspace", request.WorkspaceId);
            
            
            // verifying if the user has permission to create sprints in this workspace
            var currentUserId = _userService.UserId;
            var isWorkspaceMember = workspace.Members.Any(m => m.UserId == currentUserId);
            
            if (!isWorkspaceMember)
            {
               throw new BadRequestException("User does not have permissions to create a sprint.");
            }
            
            // Create and save sprint entity
           var sprint = request.ToSprint();
           await _sprintRepository.AddAsync(sprint, cancellationToken);
           
           return new CreateSprintCommandResult(true);
        }
    }
}
