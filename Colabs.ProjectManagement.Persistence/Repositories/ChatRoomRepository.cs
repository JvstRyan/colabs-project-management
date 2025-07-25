using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Colabs.ProjectManagement.Application.Contracts.Persistence;
using Colabs.ProjectManagement.Domain.Entities.Chat;
using Microsoft.EntityFrameworkCore;

namespace Colabs.ProjectManagement.Persistence.Repositories
{
    public class ChatRoomRepository : BaseRepository<ChatRoom>, IChatRoomRepository
    {
        public ChatRoomRepository(ColabsDbContext dbContext) : base(dbContext) { }
        public async Task<IReadOnlyList<ChatRoom>> GetAllChatRoomsByWorkspaceId(string workspaceId, CancellationToken cancellationToken = default)
        {
            var chatRooms = await _dbContext.ChatRooms
                .AsNoTracking()
                .Where(x => x.WorkspaceId == workspaceId)
                .ToListAsync(cancellationToken);

            return chatRooms;
        }
    }
}
