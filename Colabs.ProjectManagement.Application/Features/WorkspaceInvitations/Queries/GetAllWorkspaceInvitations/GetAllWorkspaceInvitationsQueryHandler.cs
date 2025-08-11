
using Colabs.ProjectManagement.Application.Contracts;
using Colabs.ProjectManagement.Application.Contracts.Persistence;
using MediatR;

namespace Colabs.ProjectManagement.Application.Features.WorkspaceInvitation.Queries.GetAllWorkspaceInvitations
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
            // 1 Gather userId of user requesting for invitations
            var userId =  _loggedInUser.UserId;

            // 2. Query database for invitations containing 
            var workspaceInvitations = await _workspaceInvitationRepository.GetWorkspaceInvitationsByUserId(userId, cancellationToken);

            // 3. Check if there are invitations

            if (workspaceInvitations is null)
                return new GetAllWorkspaceInvitationsQueryResponse(new List<GetAllWorkspaceInvitationsDto>());

            var invitationDtos = workspaceInvitations.Select(x => new GetAllWorkspaceInvitationsDto
            {
               WorkspaceInvitationId = x.WorkspaceInvitationId,
               InviterUsername = x.Inviter.Username,
               WorkspaceName = x.Workspace.Name,
               WorkspaceProfileUrl = x.Workspace.ProfileUrl
            }).ToList();

            return new GetAllWorkspaceInvitationsQueryResponse(invitationDtos);

        }
    }
}
