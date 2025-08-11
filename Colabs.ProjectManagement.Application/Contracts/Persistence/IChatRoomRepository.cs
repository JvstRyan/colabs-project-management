using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Colabs.ProjectManagement.Domain.Entities.Chat;

namespace Colabs.ProjectManagement.Application.Contracts.Persistence
{
    public interface IChatRoomRepository
    {
        Task<IReadOnlyList<ChatRoom>> GetAllChatRoomsByWorkspaceId(string workspaceId, CancellationToken cancellationToken = default);
    }
}
