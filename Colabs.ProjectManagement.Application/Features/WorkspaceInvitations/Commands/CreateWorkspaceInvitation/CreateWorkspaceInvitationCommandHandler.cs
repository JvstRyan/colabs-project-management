using Colabs.ProjectManagement.Application.Contracts;
using Colabs.ProjectManagement.Application.Contracts.Persistence;
using Colabs.ProjectManagement.Application.Exceptions;
using Colabs.ProjectManagement.Application.Mappings.WorkspaceInvitations;
using Colabs.ProjectManagement.Domain.Entities.Workspaces;
using Colabs.ProjectManagement.Domain.Enums;
using MediatR;

namespace Colabs.ProjectManagement.Application.Features.WorkspaceInvitation.Commands
{
    public class CreateWorkspaceInvitationCommandHandler : IRequestHandler<CreateWorkspaceInvitationCommand, CreateWorkspaceInvitationCommandResponse>
    {
       
        private readonly IGenericRepository<Domain.Entities.Workspaces.WorkspaceInvitation> _workspaceInvitationRepository;
        private readonly IGenericRepository<Workspace> _workspaceRepository;
        private readonly ICurrentLoggedInUserService _currentLoggedInUser;
        private readonly IUserRepository _userRepository;

        public CreateWorkspaceInvitationCommandHandler(IGenericRepository<Domain.Entities.Workspaces.WorkspaceInvitation> workspaceInvitation, ICurrentLoggedInUserService currentLoggedInUser, IUserRepository userRepository,
        IGenericRepository<Workspace> workspaceRepository)
        {
            _workspaceInvitationRepository = workspaceInvitation;
            _currentLoggedInUser = currentLoggedInUser;
            _userRepository = userRepository;
            _workspaceRepository = workspaceRepository;
        }

        public async Task<CreateWorkspaceInvitationCommandResponse> Handle(CreateWorkspaceInvitationCommand request, CancellationToken cancellationToken)
        {
                
                // 1. Check workspace id to see if it exists
                var workspace = await _workspaceRepository.GetByIdAsync(request.WorkspaceId, cancellationToken);

                if (workspace == null)
                    throw new NotFoundException("Workspace", request.WorkspaceId);
                
                // 2. Get invitee userId (email check already done in validation section)
                var user = await _userRepository.GetUserByEmailAsync(request.InviteeEmail, cancellationToken);
                Console.WriteLine(user.Email);

                // 3. Currently logged in UserId
                var currentUserId = _currentLoggedInUser.UserId;
                
                
                // 4. Create invitation
                var workspaceInvitation = new Domain.Entities.Workspaces.WorkspaceInvitation
                {
                    WorkspaceInvitationId = Guid.NewGuid().ToString(),
                    WorkspaceId = request.WorkspaceId,
                    InviterId = currentUserId,
                    InviteeId = user.UserId,
                    Status = InvitationStatus.Pending
                };
                
                await _workspaceInvitationRepository.AddAsync(workspaceInvitation, cancellationToken);

                return new CreateWorkspaceInvitationCommandResponse(true);
          
        }
    }
}
