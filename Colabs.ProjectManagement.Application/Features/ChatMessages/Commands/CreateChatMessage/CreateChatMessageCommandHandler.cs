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
            var response = new CreateChatMessageCommandResponse();

            // Don't forget to add validations here


            // 1. Checking if chatroom exists

            var chatRoom = await _chatRoomRepository.GetByIdAsync(request.ChatRoomId, cancellationToken);

            if (chatRoom == null)
            {
                response.Success = false;
                response.StatusCode = 404;
                response.Message = "Chat room could not be found";
                return response;
            }

            // 2. Forming the ChatMessage

            var userId = _currentUserService.UserId;

            var user = await _userRepository.GetByIdAsync(userId, cancellationToken);

            if (user == null)
            {
                response.Success = false;
                response.StatusCode = 404;
                response.Message = "User could not be found";
                return response;
            }

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

            response.Success = true;
            response.Message = "Succesfully created chat message";
            return response;


        }
    }
}
