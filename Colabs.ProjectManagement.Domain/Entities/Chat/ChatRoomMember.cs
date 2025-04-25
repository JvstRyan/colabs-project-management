using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colabs.ProjectManagement.Domain.Entities.Chat
{
    public class ChatRoomMember
    {
        public Guid ChatRoomMemberId { get; set; }
        public Guid ChatRoomId { get; }
        public Guid UserId { get; set; }
        public DateTime JoinedAt { get; set; }

        //Navigation properties
        public ChatRoom ChatRoom { get; private set; } = null!;
        public User User { get; private set; } = null!;
    }
}
