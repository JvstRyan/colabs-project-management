using Colabs.ProjectManagement.Domain.Entities.Chat;

namespace Colabs.ProjectManagement.Application.Contracts.Persistence
{
    public interface IChatMessageRepository
    {
        Task<IReadOnlyList<ChatMessage>> GetAllChatMessagesForChatRoomAsync(string chatRoomId, CancellationToken cancellationToken = default);
    }
}
