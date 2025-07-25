using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Primitives;

namespace Colabs.ProjectManagement.Application.Features.ChatMessages.Queries.GetAllChatMessages
{
    public class GetAllChatMessagesQueryDto
    {
        public string ChatMessageId { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public string AvatarUrl { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;

    }
}
