using Colabs.ProjectManagement.Application.Contracts.Persistence;
using Colabs.ProjectManagement.Domain.Entities.Workspaces;
using MediatR;

namespace Colabs.ProjectManagement.Application.Features.WorkspaceInvitations.Commands.UpdateWorkspaceInvitation
{
    public class UpdateWorkspaceInvitationCommandHandler : IRequestHandler<UpdateWorkspaceInvitationCommand, UpdateWorkspaceInvitationCommandResponse>
    {
        private readonly IGenericRepository<WorkspaceInvitation> _workspaceInvitationRepository;

        public UpdateWorkspaceInvitationCommandHandler(IGenericRepository<WorkspaceInvitation> workspaceInvitationRepository)
        {
            _workspaceInvitationRepository = workspaceInvitationRepository;
        }
        
        public async Task<UpdateWorkspaceInvitationCommandResponse> Handle(UpdateWorkspaceInvitationCommand request, CancellationToken cancellationToken)
        {
            var response = new UpdateWorkspaceInvitationCommandResponse();
            
            try
            {
                var validator = new UpdateWorkspaceInvitationCommandValidator();
                var validationResult = await validator.ValidateAsync(request, cancellationToken);
                
                if (!validationResult.IsValid)
                {
                    response.Success = false;
                    response.StatusCode = 400;
                    response.ValidationErrors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                    return response;
                }
                
                // 1. Find workspace invitation
                var workspaceInvitation = await _workspaceInvitationRepository.GetByIdAsync(request.WorkspaceInvitationId, cancellationToken);

                if (workspaceInvitation == null)
                {
                    response.Success = false;
                    response.StatusCode = 404;
                    response.Message = "Workspace invitation could not be found";
                    return response;
                }
                
                workspaceInvitation.Status = request.InvitationStatus;
                await _workspaceInvitationRepository.UpdateAsync(workspaceInvitation, cancellationToken);
                
                response.Success = true;
                response.Message = "Successfully updated workspace invitation";
                return response;
                
                
                
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = $"Something went wrong while trying to update the workspace invitation: {ex.Message}";
                response.StatusCode = 500;
                return response;
            }
        }
    }
}
