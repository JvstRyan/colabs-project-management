using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Colabs.ProjectManagement.Domain.Entities.Chat;

namespace Colabs.ProjectManagement.Application.Contracts.WebSocket
{
    public interface IChatMessageNotifier
    {
        Task NotifyMessageCreatedAsync(ChatMessage chatMessage, CancellationToken cancellationToken);
    }
}
