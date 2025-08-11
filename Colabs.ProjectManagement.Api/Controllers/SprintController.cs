using Colabs.ProjectManagement.Application.Features.Sprints.Commands;
using Colabs.ProjectManagement.Application.Features.Sprints.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Colabs.ProjectManagement.Api.Controllers
{
    [ApiController]
    [Route("api/workspaces/{workspaceId}/sprints")]
    public class SprintController : ControllerBase
    {
      private readonly IMediator _mediator;

      public SprintController(IMediator mediator)
      {
          _mediator = mediator;
      }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<CreateSprintCommandResult>> CreateSprint
            ([FromRoute] string workspaceId, [FromBody] CreateSprintCommand createSprintCommand)
        {
            createSprintCommand.WorkspaceId = workspaceId;

            var result = await _mediator.Send(createSprintCommand);
            
            return CreatedAtAction(nameof(GetAllSprints), new {workspaceId}, result);
        }


        [Authorize]
        [HttpGet]
        public async Task<ActionResult<GetAllSprintsQueryResponse>> GetAllSprints([FromRoute] string workspaceId)
        {
            var query = new GetAllSprintsQuery() { WorkspaceId = workspaceId };

            var response = await _mediator.Send(query);
            
            return Ok(response);

        }



    }
}
