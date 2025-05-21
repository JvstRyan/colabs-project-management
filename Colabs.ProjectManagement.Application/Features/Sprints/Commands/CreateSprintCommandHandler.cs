using Colabs.ProjectManagement.Application.Contracts;
using Colabs.ProjectManagement.Application.Contracts.Persistence;
using Colabs.ProjectManagement.Application.Mappings.Sprints;
using Colabs.ProjectManagement.Domain.Entities.TaskManagement;
using MediatR;

namespace Colabs.ProjectManagement.Application.Features.Sprints.Commands
{
    public class CreateSprintCommandHandler : IRequestHandler<CreateSprintCommand, CreateSprintCommandResponse>
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

        public async Task<CreateSprintCommandResponse> Handle(CreateSprintCommand request, CancellationToken cancellationToken)
        {
            var response = new CreateSprintCommandResponse();
            
            // validating incoming request to create sprint
            var validator = new CreateSprintValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
            {
                   response.Success = false;
                   response.StatusCode = 400;
                   response.Message = "Provided details are invalid for sprint creation";
                   response.ValidationErrors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                   return response;
            }
            
            // verifying if the workspace exists
            var workspace = await _workspaceRepository.GetWorkspaceMembersAsync(request.WorkspaceId, cancellationToken);
            if (workspace == null)
            {
                response.Success = false;
                response.StatusCode = 404;
                response.ValidationErrors = new List<string> { "Workspace not found"};
                return response;
            }
            
            // verifying if the user has permission to create sprints in this workspace
            var currentUserId = _userService.UserId;
            var isWorkspaceMember = workspace.Members.Any(m => m.UserId == currentUserId);
            
            if (!isWorkspaceMember)
            {
                response.Success = false;
                response.StatusCode = 403;
                response.ValidationErrors = new List<string> { "You do not have permission to create sprints in this workspace" };
                return response;
            }
            
            // Create and save sprint entity
           var sprint = request.ToSprint();
           await _sprintRepository.AddAsync(sprint, cancellationToken);
            
           response.Success = true;
           response.Message = $"Sprint {sprint.SprintId} has been created";
           return response;
        }
    }
}
