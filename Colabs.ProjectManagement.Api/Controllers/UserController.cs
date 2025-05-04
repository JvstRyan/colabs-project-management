using Colabs.ProjectManagement.Application.Features.Auth.Commands.Login;
using Colabs.ProjectManagement.Application.Features.Auth.Commands.Register;
using Colabs.ProjectManagement.Application.Features.Auth.Queries.GetUserById;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Colabs.ProjectManagement.Api.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("details/{id}", Name = "GetUserById")]
        public async Task<ActionResult<GetUserByIdResponse>> GetUserById(string id)
        {
            var getUserByIdQuery = new GetUserByIdQuery() {UserId = id};
            var result = await _mediator.Send(getUserByIdQuery);
            return Ok(result);
        }

        [HttpPost("auth/register", Name = "RegisterUser")]
        public async Task<ActionResult<RegisterUserCommandResponse>> RegisterUser(
            [FromBody] RegisterUserCommand registerUserCommand)
        {
          var response = await _mediator.Send(registerUserCommand);
          return Ok(response);
        }

        [HttpPost("auth/login", Name = "LoginUser")]
        public async Task<ActionResult<LoginUserCommandResponse>> LoginUser([FromBody] LoginUserCommand loginUserCommand)
        {
            var response = await _mediator.Send(loginUserCommand);
            return Ok(response);
        }
    }
}
