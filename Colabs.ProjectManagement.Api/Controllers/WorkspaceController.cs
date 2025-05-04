using Colabs.ProjectManagement.Application.Features.Workspaces.Commands.CreateWorkspace;
using MediatR;
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
        
        [HttpPost(Name = "CreateWorkspace")]
        public async Task<ActionResult<CreateWorkspaceCommandResponse>> Create(
            [FromBody] CreateWorkspaceCommand createWorkspaceCommand)
        {
            var response = await _mediator.Send(createWorkspaceCommand);
            return Ok(response);
        }
      
    }
}
