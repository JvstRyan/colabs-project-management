using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colabs.ProjectManagement.Domain.Entities.Chat
{
    public class ChatRoomMember
    {
        public string ChatRoomMemberId { get; set; } = string.Empty;
        public string ChatRoomId { get; set; } = string.Empty;
        public string UserId { get; set; } = string.Empty;
        public DateTime JoinedAt { get; set; }

        //Navigation properties
        public ChatRoom ChatRoom { get; private set; } = null!;
        public User User { get; private set; } = null!;
    }
}
