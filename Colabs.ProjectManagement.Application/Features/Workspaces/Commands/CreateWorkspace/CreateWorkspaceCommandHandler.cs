using Colabs.ProjectManagement.Application.Contracts;
using Colabs.ProjectManagement.Application.Contracts.Persistence;
using Colabs.ProjectManagement.Application.Mappings;
using Colabs.ProjectManagement.Domain.Entities.Workspaces;
using MediatR;

namespace Colabs.ProjectManagement.Application.Features.Workspaces.Commands.CreateWorkspace
{
    public class CreateWorkspaceCommandHandler : IRequestHandler<CreateWorkspaceCommand, CreateWorkspaceCommandResponse>
    {
        private readonly IGenericRepository<Workspace> _workspaceRepository;
        private readonly ICurrentLoggedInUserService _currentLoggedInUser;

        public CreateWorkspaceCommandHandler(IGenericRepository<Workspace> workspaceRepository, ICurrentLoggedInUserService currentLoggedInUser)
        {
            _workspaceRepository = workspaceRepository;
            _currentLoggedInUser = currentLoggedInUser;
        }
        public async Task<CreateWorkspaceCommandResponse> Handle(CreateWorkspaceCommand request, CancellationToken cancellationToken)
        {
           var validator = new CreateWorkspaceCommandValidator();
           var validationResult = await validator.ValidateAsync(request, cancellationToken);

           if (!validationResult.IsValid)
           {
               return new CreateWorkspaceCommandResponse
               {
                   Success = false,
                   StatusCode = 400,
                   Message = "Provided details are invalid for workspace creation",
                   ValidationErrors = validationResult.Errors.Select(e => e.ErrorMessage).ToList()
                   
               };
           }
           
           var workspace = request.ToWorkspace(_currentLoggedInUser.UserId);
           
           var adminRole = RoleMapper.CreateAdminRole(workspace.WorkspaceId);
           var guestRole = RoleMapper.CreateGuestRole(workspace.WorkspaceId);
           
           workspace.Roles.Add(adminRole);
           workspace.Roles.Add(guestRole);
           
           var creatorMember = workspace.ToWorkspaceMember(adminRole);
           
           workspace.Members.Add(creatorMember);
           
           await _workspaceRepository.AddAsync(workspace, cancellationToken);
           
           return workspace.ToCreateWorkspaceCommandResponse();
        }
    }
}
