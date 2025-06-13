using Colabs.ProjectManagement.Application.Contracts.Persistence;
using Colabs.ProjectManagement.Application.Features.Workspaces.Commands.CreateWorkspace;
using Colabs.ProjectManagement.Application.Mappings;
using Colabs.ProjectManagement.Domain.Entities;
using Colabs.ProjectManagement.Domain.Entities.Workspaces;
using MediatR;

namespace Colabs.ProjectManagement.Application.Features.WorkspaceMembers.Commands.CreateWorkspaceMember
{
    public class CreateWorkspaceMemberCommandHandler : IRequestHandler<CreateWorkspaceMemberCommand, CreateWorkspaceMemberCommandResponse>
    {
        private readonly IGenericRepository<WorkspaceInvitation> _workspaceInvitationRepository;
        private readonly IGenericRepository<WorkspaceMember> _workspaceMemberRepository;
        private readonly IRoleRepository _roleRepository;

        public CreateWorkspaceMemberCommandHandler(IGenericRepository<WorkspaceInvitation> workspaceInvitationRepository, IGenericRepository<WorkspaceMember> workspaceMemberRepository,
            IRoleRepository roleRepository)
        {
            _workspaceInvitationRepository = workspaceInvitationRepository;
            _workspaceMemberRepository = workspaceMemberRepository;
            _roleRepository = roleRepository;
        }

        public async Task<CreateWorkspaceMemberCommandResponse> Handle(CreateWorkspaceMemberCommand request, CancellationToken cancellationToken)
        {
            var response = new CreateWorkspaceMemberCommandResponse();
            
            try
            {
                var validator = new CreateWorkspaceMemberCommandValidator();
                var validationResult = await validator.ValidateAsync(request, cancellationToken);

                if (!validationResult.IsValid)
                {
                    response.Success = false;
                    response.StatusCode = 400;
                    response.ValidationErrors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                    return response;
                }
                
                // 1. Find workspace by workspaceInvitationId
                var workspaceInvitation = await _workspaceInvitationRepository.GetByIdAsync(request.WorkspaceInvitationId, cancellationToken);

                if (workspaceInvitation == null)
                {
                    response.Success = false;
                    response.StatusCode = 404;
                    response.Message = "Workspace invitation could not be found";
                    return response;
                }
                
                // 2. Get guest role
                var guestRole = await _roleRepository.GetRoleByName(workspaceInvitation.WorkspaceId, "Guest", cancellationToken);
                
                // 3. Build workspaceMember based on invitation details
                var workspaceMember = new WorkspaceMember
                {
                    WorkspaceId = workspaceInvitation.WorkspaceId,
                    UserId = workspaceInvitation.InviteeId,
                    RoleId = guestRole.RoleId,
                    JoinedAt = DateTime.UtcNow
                };
                
                // 4. Save workspace member
                await _workspaceMemberRepository.AddAsync(workspaceMember, cancellationToken);
                
                response.Success = true;
                response.Message = "Successfully created workspace member";
                return response;
                
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = $"Something went wrong while trying to create a workspace member: {ex.Message}";
                response.StatusCode = 500;
                return response;
            }
        }
    }
}
