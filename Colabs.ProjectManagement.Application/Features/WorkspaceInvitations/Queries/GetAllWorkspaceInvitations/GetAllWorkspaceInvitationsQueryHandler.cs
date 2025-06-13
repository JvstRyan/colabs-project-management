using Colabs.ProjectManagement.Application.Contracts;
using Colabs.ProjectManagement.Application.Contracts.Persistence;
using MediatR;

namespace Colabs.ProjectManagement.Application.Features.WorkspaceInvitations.Queries.GetAllWorkspaceInvitations
{
    public class GetAllWorkspaceInvitationsQueryHandler : IRequestHandler<GetAllWorkspaceInvitationsQuery, GetAllWorkspaceInvitationsQueryResponse>
    {
        private readonly IWorkspaceInvitationRepository _workspaceInvitationRepository;
        private readonly ICurrentLoggedInUserService _loggedInUser;

        public GetAllWorkspaceInvitationsQueryHandler(IWorkspaceInvitationRepository workspaceInvitationRepository, ICurrentLoggedInUserService loggedInUser)
        {
            _workspaceInvitationRepository = workspaceInvitationRepository;
            _loggedInUser = loggedInUser;
        }

        public async Task<GetAllWorkspaceInvitationsQueryResponse> Handle(GetAllWorkspaceInvitationsQuery request, CancellationToken cancellationToken)
        {
           var response = new GetAllWorkspaceInvitationsQueryResponse();

           try
           {
               // 1 Gather userId of user requesting for invitations
               var userId =  _loggedInUser.UserId;
               
               // 2 Query database for invitations containing 
               var workspaceInvitations = await _workspaceInvitationRepository.GetWorkspaceInvitationsByUserId(userId, cancellationToken);
               
               // 3 Continue with logic if value is not null
               if (workspaceInvitations != null)
               {
                   var invitationDtos = workspaceInvitations.Select(x => new GetAllWorkspaceInvitationsDto
                   {
                       WorkspaceInvitationId = x.WorkspaceInvitationId,
                       InviterUsername = x.Inviter.Username,
                       WorkspaceName = x.Workspace.Name,
                       WorkspaceProfileUrl = x.Workspace.ProfileUrl
                   }).ToList();
                   
                   response.Success = true;
                   response.Message = "WorkspaceInvitations retrieved successfully";
                   response.Invitations = invitationDtos;
                   return response;
               }
              
               response.Success = true;
               response.Message = "No workspace invitations were found";
               return response;
           }
           catch (Exception ex)
           {
               response.Success = false;
               response.Message = $"Something went wrong while trying to retrieve workspace invitations: {ex.Message}";
               response.StatusCode = 500;
               return response;
           }
        }
    }
}
