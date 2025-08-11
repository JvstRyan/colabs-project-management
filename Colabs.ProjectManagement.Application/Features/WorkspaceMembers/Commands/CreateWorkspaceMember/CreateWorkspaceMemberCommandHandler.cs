using Colabs.ProjectManagement.Application.Contracts.Persistence;
using Colabs.ProjectManagement.Domain.Entities.Workspaces;
using MediatR;

namespace Colabs.ProjectManagement.Application.Features.WorkspaceMembers.Commands.CreateWorkspaceMember
{
    public class CreateWorkspaceMemberCommandHandler : IRequestHandler<CreateWorkspaceMemberCommand, CreateWorkspaceMemberCommandResponse>
    {
        private readonly IGenericRepository<Domain.Entities.Workspaces.WorkspaceInvitation> _workspaceInvitationRepository;
        private readonly IGenericRepository<WorkspaceMember> _workspaceMemberRepository;
        private readonly IRoleRepository _roleRepository;

        public CreateWorkspaceMemberCommandHandler(IGenericRepository<Domain.Entities.Workspaces.WorkspaceInvitation> workspaceInvitationRepository, IGenericRepository<WorkspaceMember> workspaceMemberRepository,
            IRoleRepository roleRepository)
        {
            _workspaceInvitationRepository = workspaceInvitationRepository;
            _workspaceMemberRepository = workspaceMemberRepository;
            _roleRepository = roleRepository;
        }

        public async Task<CreateWorkspaceMemberCommandResponse> Handle(CreateWorkspaceMemberCommand request, CancellationToken cancellationToken)
        {
           
            // 1. Find workspace by workspaceInvitationId
            var workspaceInvitation = await _workspaceInvitationRepository.GetByIdAsync(request.WorkspaceInvitationId, cancellationToken);

            if (workspaceInvitation is null)
                throw new NotFoundException("WorkspaceInvitation", request.WorkspaceInvitationId);
                
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

            return new CreateWorkspaceMemberCommandResponse(true);   
            
        }
    }
}
