using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace Colabs.ProjectManagement.Application.Features.ChatRooms.Commands.CreateChatRoomCommand
{
    public class CreateChatRoomCommand : IRequest<CreateChatRoomCommandResponse>
    {
        public string WorkspaceId { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;

    }
}
