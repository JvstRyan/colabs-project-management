using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Colabs.ProjectManagement.Domain.Common;

namespace Colabs.ProjectManagement.Domain.Entities.Chat
{
    public class ChatMessage : AuditableEntity
    {
        public Guid ChatMessageId { get; set; }
        public Guid ChatRoomId { get; set; }
        public Guid UserId { get; set; }
        public string Content { get; set; } = string.Empty;

        //Navigation properties
        public ChatRoom ChatRoom { get; private set; } = null!;
        public User User { get; private set; } = null!;


    }
}
