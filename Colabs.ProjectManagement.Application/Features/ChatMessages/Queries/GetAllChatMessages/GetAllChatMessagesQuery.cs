using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace Colabs.ProjectManagement.Application.Features.ChatMessages.Queries.GetAllChatMessages
{
    public class GetAllChatMessagesQuery : IRequest<GetAllChatMessagesQueryResponse>
    {
        public string ChatRoomId { get; set; } = string.Empty;
    }
}
