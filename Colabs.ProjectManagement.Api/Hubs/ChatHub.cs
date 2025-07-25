using Microsoft.AspNetCore.SignalR;

namespace Colabs.ProjectManagement.Api.Hubs
{
    public class ChatHub : Hub
    {
        public async Task JoinRoom(string chatRoomId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, chatRoomId);
        }

        public async Task LeaveRoom(string chatRoomId)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, chatRoomId);
        }
    }
}
