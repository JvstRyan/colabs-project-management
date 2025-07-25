using Colabs.ProjectManagement.Api.Hubs;
using Colabs.ProjectManagement.Application.Contracts.WebSocket;
using Colabs.ProjectManagement.Domain.Entities.Chat;
using Microsoft.AspNetCore.SignalR;

namespace Colabs.ProjectManagement.Api.Notifications
{
    public class SignalRChatMessageNotifier : IChatMessageNotifier
    {
        private readonly IHubContext<ChatHub> _hubContext;

        public SignalRChatMessageNotifier(IHubContext<ChatHub> hubContext)
        {
            _hubContext = hubContext;
        }

        public async Task NotifyMessageCreatedAsync(ChatMessage chatMessage, CancellationToken cancellationToken)
        {
            var messagePayload = new
            {
                chatMessage.ChatMessageId,
                chatMessage.ChatRoomId,
                chatMessage.Content,
                chatMessage.UserId,

                chatMessage.User?.Username,
                chatMessage.User?.AvatarUrl,
            };

            await _hubContext.Clients
                .Group(chatMessage.ChatRoomId)
                .SendAsync("ReceiveMessage", messagePayload, cancellationToken);
        }
    }
}
