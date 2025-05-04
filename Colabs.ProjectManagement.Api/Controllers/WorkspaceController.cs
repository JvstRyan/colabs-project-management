using Colabs.ProjectManagement.Application.Features.Workspaces.Commands.CreateWorkspace;
using Colabs.ProjectManagement.Application.Features.Workspaces.Queries.GetAllWorkspaces;
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

        [HttpGet(Name = "GetAllWorkspacesForUser")]
        public async Task<ActionResult<GetAllWorkspacesQueryResponse>> GetAll()
        {
            var getAllWorkspacesQuery = new GetAllWorkspacesQuery();
            var result = await _mediator.Send(getAllWorkspacesQuery);
            return Ok(result);
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
