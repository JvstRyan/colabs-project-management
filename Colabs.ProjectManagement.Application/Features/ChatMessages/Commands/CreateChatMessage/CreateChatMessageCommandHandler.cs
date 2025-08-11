using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Colabs.ProjectManagement.Application.Contracts;
using Colabs.ProjectManagement.Application.Contracts.Persistence;
using Colabs.ProjectManagement.Application.Contracts.WebSocket;
using Colabs.ProjectManagement.Domain.Entities;
using Colabs.ProjectManagement.Domain.Entities.Chat;
using MediatR;

namespace Colabs.ProjectManagement.Application.Features.ChatMessages.Commands.CreateChatMessage
{
    public class CreateChatMessageCommandHandler : IRequestHandler<CreateChatMessageCommand, CreateChatMessageCommandResponse>
    {
        private readonly IGenericRepository<ChatMessage> _chatMessageRepository;
        private readonly IGenericRepository<ChatRoom> _chatRoomRepository;
        private readonly ICurrentLoggedInUserService _currentUserService;
        private readonly IGenericRepository<User> _userRepository;
        private readonly IChatMessageNotifier _notifier;

        public CreateChatMessageCommandHandler(IGenericRepository<ChatMessage> chatMessageRepository, IGenericRepository<ChatRoom> chatRoomRepository,
            ICurrentLoggedInUserService currentUserService, IChatMessageNotifier notifier, IGenericRepository<User> userRepository)
        {
            _chatMessageRepository = chatMessageRepository;
            _chatRoomRepository = chatRoomRepository;
            _currentUserService = currentUserService;
            _notifier = notifier;
            _userRepository = userRepository;
        }

        public async Task<CreateChatMessageCommandResponse> Handle(CreateChatMessageCommand request, CancellationToken cancellationToken)
        {
            // 1. Checking if chatroom exists

            var chatRoom = await _chatRoomRepository.GetByIdAsync(request.ChatRoomId, cancellationToken);

            if (chatRoom is null)
                throw new NotFoundException(nameof(chatRoom), request.ChatRoomId);

            // 2. Forming the ChatMessage

            var userId = _currentUserService.UserId;

            var user = await _userRepository.GetByIdAsync(userId, cancellationToken);

            if (user is null)
                throw new NotFoundException("User", userId);

            var chatMessage = new ChatMessage
            {
                ChatMessageId = Guid.NewGuid().ToString(),
                ChatRoomId = request.ChatRoomId,
                UserId = userId,
                Content = request.Content,
            };

            chatMessage.SetUser(user);

            // 3. Sending request

            await _chatMessageRepository.AddAsync(chatMessage, cancellationToken);

            // 4. Notifying SignalR

            await _notifier.NotifyMessageCreatedAsync(chatMessage, cancellationToken);

            return new CreateChatMessageCommandResponse(true);
        }
    }
}