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
    public class ChatMessageRepository : BaseRepository<ChatMessage>, IChatMessageRepository
    {
        public ChatMessageRepository(ColabsDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<IReadOnlyList<ChatMessage>> GetAllChatMessagesForChatRoomAsync(string chatRoomId, CancellationToken cancellationToken = default)
        {
            return await _dbContext.ChatMessage
                 .AsNoTracking()
                 .Include(x => x.User)
                 .Where(x => x.ChatRoomId == chatRoomId)
                 .OrderBy(x => x.CreatedDate)
                 .ToListAsync(cancellationToken);
        }
    }
}
