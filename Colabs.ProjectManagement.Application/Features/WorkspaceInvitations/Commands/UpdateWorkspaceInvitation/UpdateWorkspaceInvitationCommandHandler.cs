using Colabs.ProjectManagement.Application.Contracts.Persistence;
using Colabs.ProjectManagement.Application.Exceptions;
using Colabs.ProjectManagement.Domain.Entities.Workspaces;
using MediatR;

namespace Colabs.ProjectManagement.Application.Features.WorkspaceInvitation.Commands.UpdateWorkspaceInvitation
{
    public class UpdateWorkspaceInvitationCommandHandler : IRequestHandler<UpdateWorkspaceInvitationCommand, UpdateWorkspaceInvitationCommandResponse>
    {
        private readonly IGenericRepository<Domain.Entities.Workspaces.WorkspaceInvitation> _workspaceInvitationRepository;

        public UpdateWorkspaceInvitationCommandHandler(IGenericRepository<Domain.Entities.Workspaces.WorkspaceInvitation> workspaceInvitationRepository)
        {
            _workspaceInvitationRepository = workspaceInvitationRepository;
        }
        
        public async Task<UpdateWorkspaceInvitationCommandResponse> Handle(UpdateWorkspaceInvitationCommand request, CancellationToken cancellationToken)
        {
           
                
            // 1. Find workspace invitation
            var workspaceInvitation = await _workspaceInvitationRepository.GetByIdAsync(request.WorkspaceInvitationId, cancellationToken);

            if (workspaceInvitation is null)
                throw new NotFoundException("Workspace Invitation", request.WorkspaceInvitationId);
                
            workspaceInvitation.Status = request.InvitationStatus;
            await _workspaceInvitationRepository.UpdateAsync(workspaceInvitation, cancellationToken);

            return new UpdateWorkspaceInvitationCommandResponse(true);
                
        }
    }
}
