﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Colabs.ProjectManagement.Domain.Common;

namespace Colabs.ProjectManagement.Domain.Entities.Chat
{
    public class ChatMessage : AuditableEntity
    {
        public string ChatMessageId { get; set; } = string.Empty;
        public string ChatRoomId { get; set; } = string.Empty;
        public string UserId { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;

        //Navigation properties
        public ChatRoom ChatRoom { get; private set; } = null!;
        public User User { get; private set; } = null!;

        public void SetUser(User user)
        {
            if (user == null || user.UserId != this.UserId)
            {
                throw new ArgumentException("Provided user is null or does not match the UserId.", nameof(user));
            }

            this.User = user;
        }
        

    }
}
