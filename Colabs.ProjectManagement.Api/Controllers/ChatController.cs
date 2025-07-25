using Colabs.ProjectManagement.Application.Features.ChatMessages.Commands.CreateChatMessage;
using Colabs.ProjectManagement.Application.Features.ChatMessages.Queries.GetAllChatMessages;
using Colabs.ProjectManagement.Application.Features.ChatRooms.Commands.CreateChatRoomCommand;
using Colabs.ProjectManagement.Application.Features.ChatRooms.Queries.GetAllChatRoomsQuery;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Colabs.ProjectManagement.Api.Controllers
{
    [ApiController]
    [Route("api/workspaces")]
    public class ChatController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ChatController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Authorize]
        [HttpPost("{id}/chat-rooms", Name = "CreateChatRoom")]
        public async Task<ActionResult<CreateChatRoomCommandResponse>> CreateChatRoom([FromBody] CreateChatRoomCommand chatRoom)
        {
            var command = new CreateChatRoomCommand() { WorkspaceId = chatRoom.WorkspaceId, Name = chatRoom.Name };

            var response = await _mediator.Send(command);
            return StatusCode(response.StatusCode, response);
        }

        [Authorize]
        [HttpGet("{id}/chat-rooms", Name = "GetChatRooms")]
        public async Task<ActionResult<GetAllChatRoomsQueryResponse>> GetAllChatRooms(string id)
        {
            var query = new GetAllChatRoomsQuery() { WorkspaceId = id };

            var response = await _mediator.Send(query);
            return StatusCode(response.StatusCode, response);
        }

        [Authorize]
        [HttpPost("chat-rooms/{id}/messages", Name = "CreateChatMessages")]
        public async Task<ActionResult<CreateChatMessageCommandResponse>> CreateChatMessage(string id, [FromBody] CreateChatMessageCommand command)
        {
            command.ChatRoomId = id;
            
            var result = await _mediator.Send(command);
            return StatusCode(result.StatusCode, result);
        }

        [Authorize]
        [HttpGet("chat-rooms/{id}/messages", Name = "GetAllMessages")]
        public async Task<ActionResult<GetAllChatMessagesQueryResponse>> GetAllChatMessages(string id)
        {
            var query = new GetAllChatMessagesQuery() { ChatRoomId = id };

            var result = await _mediator.Send(query);
            return StatusCode(result.StatusCode, result);
        }



      
    }
}
