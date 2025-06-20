﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Colabs.ProjectManagement.Domain.Common;
using Colabs.ProjectManagement.Domain.Entities.Workspaces;

namespace Colabs.ProjectManagement.Domain.Entities.Chat
{
    public class ChatRoom : AuditableEntity
    {
        public string ChatRoomId { get; set; } = string.Empty;
        public string WorkspaceId { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;

        //Navigation properties
        public Workspace Workspace { get; private set; } = null!;
        public ICollection<ChatRoomMember> Members { get; private set; } = new List<ChatRoomMember>();
        public ICollection<ChatMessage> Messages { get; private set; } = new List<ChatMessage>();
        
    }
}
