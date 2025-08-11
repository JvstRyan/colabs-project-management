using Colabs.ProjectManagement.Application.Features.WorkspaceInvitation.Commands;
using Colabs.ProjectManagement.Application.Features.WorkspaceInvitation.Commands.UpdateWorkspaceInvitation;
using Colabs.ProjectManagement.Application.Features.WorkspaceInvitation.Queries.GetAllWorkspaceInvitations;
using Colabs.ProjectManagement.Application.Features.WorkspaceMembers.Commands.CreateWorkspaceMember;
using Colabs.ProjectManagement.Application.Features.WorkspaceMembers.Queries.GetAllWorkspaceMembers;
using Colabs.ProjectManagement.Application.Features.Workspaces.Commands.CreateWorkspace;
using Colabs.ProjectManagement.Application.Features.Workspaces.Commands.UploadWorkpsaceProfilePicture;
using Colabs.ProjectManagement.Application.Features.Workspaces.Commands.UploadWorkspaceBanner;
using Colabs.ProjectManagement.Application.Features.Workspaces.Queries.GetAllWorkspaces;
using Colabs.ProjectManagement.Application.Features.Workspaces.Queries.GetWorkspace;
using Colabs.ProjectManagement.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Colabs.ProjectManagement.Api.Controllers
{
    [ApiController]
    [Route("api/workspaces")]
    public class WorkspaceController : ControllerBase
    { 
        private readonly IMediator _mediator;

        public WorkspaceController(IMediator mediator)
        {
            _mediator = mediator;
        }
    
        [Authorize]
        [HttpGet(Name = "GetAllWorkspacesForUser")]
        public async Task<ActionResult<GetAllWorkspacesQueryResponse>> GetAll()
        {
            var getAllWorkspacesQuery = new GetAllWorkspacesQuery();
            var result = await _mediator.Send(getAllWorkspacesQuery);
            return StatusCode(result.StatusCode, result);
        }

        [Authorize]
        [HttpGet("{id}", Name = "GetWorkspaceById")]
        public async Task<ActionResult<GetWorkspaceQueryResponse>> GetWorkspaceById(string id)
        {
            var getWorkspaceQuery = new GetWorkspaceQuery
            {
                WorkspaceId = id
            };
            
            var response = await _mediator.Send(getWorkspaceQuery);
            return StatusCode(response.StatusCode, response);
        }
        
        [Authorize]
        [HttpPost(Name = "CreateWorkspace")]
        public async Task<ActionResult<CreateWorkspaceCommandResponse>> Create(
            [FromBody] CreateWorkspaceCommand createWorkspaceCommand)
        {
            var response = await _mediator.Send(createWorkspaceCommand);
            return StatusCode(response.StatusCode, response);
        }

        [Authorize]
        [HttpPost("{id}/profile-picture", Name = "UploadWorkspaceProfile")]
        public async Task<ActionResult> UploadProfilePicture(string id, IFormFile file)
        {
            var command = new UploadWorkspaceProfilePictureCommand
            {
                WorkspaceId = id,
                File = file
            };
            
            var response = await _mediator.Send(command);
            return StatusCode(response.StatusCode, response);
        }

        [Authorize]
        [HttpPost("{id}/banner", Name = "UploadWorkspaceBanner")]
        public async Task<ActionResult> UploadWorkspaceBanner(string id, IFormFile file)
        {
            var command = new UploadWorkspaceBannerCommand
            {
                WorkspaceId = id,
                File = file
            };
            
            var response = await _mediator.Send(command);
            return StatusCode(response.StatusCode, response);
        }

        [Authorize]
        [HttpPost("workspace-invitation", Name = "WorkspaceInvitation")]
        public async Task<ActionResult<CreateWorkspaceInvitationCommandResponse>>
            CreateWorkspaceInvitation([FromBody] CreateWorkspaceInvitationCommand createWorkspaceInvitationCommand)
        {
            var response = await _mediator.Send(createWorkspaceInvitationCommand);
            return StatusCode(response.StatusCode, response);
        }

        [Authorize]
        [HttpGet("workspace-invitation", Name = "GetWorkspaceInvitations")]
        public async Task<ActionResult<GetAllWorkspaceInvitationsQueryResponse>> GetAllWorkspaceInvitations()
        {
            var getAllWorkspaceInvitations = new GetAllWorkspaceInvitationsQuery();
            var response = await _mediator.Send(getAllWorkspaceInvitations);
            return StatusCode(response.StatusCode, response);
        }

        [Authorize]
        [HttpPost("workspace-invitation/{id}/accept", Name = "WorkspaceInvitationAccepted")]
        public async Task<ActionResult<CreateWorkspaceMemberCommandResponse>> CreateWorkspaceMember(string id)
        {
            var command = new CreateWorkspaceMemberCommand
            {
                WorkspaceInvitationId = id
            };
            
            var response = await _mediator.Send(command);

            if (response.StatusCode == 200)
            {
                var updateCommand = new UpdateWorkspaceInvitationCommand()
                {
                    WorkspaceInvitationId = id,
                    InvitationStatus = InvitationStatus.Accepted
                };
                await _mediator.Send(updateCommand);
            }
            
            return StatusCode(response.StatusCode, response);
           
        }

        [Authorize]
        [HttpGet("{id}/workspace-members", Name = "GetAllWorkspaceMembers")]
        public async Task<ActionResult<GetAllWorkspaceMembersQueryResponse>> GetAllWorkspaceMembers(string id)
        {
            var query = new GetAllWorkspaceMembersQuery() {WorkspaceId = id};
            
            var response = await _mediator.Send(query);
            return StatusCode(response.StatusCode, response);
        }
        
    }
}
