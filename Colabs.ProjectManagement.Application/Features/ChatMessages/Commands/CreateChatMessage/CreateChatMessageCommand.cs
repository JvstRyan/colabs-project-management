using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Colabs.ProjectManagement.Domain.Entities.Chat;
using MediatR;

namespace Colabs.ProjectManagement.Application.Features.ChatMessages.Commands.CreateChatMessage
{
    public class CreateChatMessageCommand : IRequest<CreateChatMessageCommandResponse>
    {
        public string ChatRoomId { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
    }
}
