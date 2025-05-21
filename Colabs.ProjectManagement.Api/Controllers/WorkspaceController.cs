using Colabs.ProjectManagement.Application.Features.Workspaces.Commands.CreateWorkspace;
using Colabs.ProjectManagement.Application.Features.Workspaces.Commands.UploadWorkpsaceProfilePicture;
using Colabs.ProjectManagement.Application.Features.Workspaces.Queries.GetAllWorkspaces;
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
      
    }
}
