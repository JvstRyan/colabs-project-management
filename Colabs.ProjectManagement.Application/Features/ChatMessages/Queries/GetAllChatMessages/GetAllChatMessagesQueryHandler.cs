using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Colabs.ProjectManagement.Application.Contracts.Persistence;
using Colabs.ProjectManagement.Domain.Entities.Chat;
using MediatR;

namespace Colabs.ProjectManagement.Application.Features.ChatMessages.Queries.GetAllChatMessages
{
    public class GetAllChatMessagesQueryHandler : IRequestHandler<GetAllChatMessagesQuery, GetAllChatMessagesQueryResponse>
    {
        private readonly IChatMessageRepository _chatMessageRepository;
        private readonly IGenericRepository<ChatRoom> _chatRoomRepository;

        public GetAllChatMessagesQueryHandler(IChatMessageRepository chatMessageRepository, IGenericRepository<ChatRoom> chatRoomRepository)
        {
            _chatMessageRepository = chatMessageRepository;
            _chatRoomRepository = chatRoomRepository;   
        }
        public async Task<GetAllChatMessagesQueryResponse> Handle(GetAllChatMessagesQuery request, CancellationToken cancellationToken)
        {
            var response = new GetAllChatMessagesQueryResponse();

            // Add validations here

            // 1. Check room

            var chatRoom = await _chatRoomRepository.GetByIdAsync(request.ChatRoomId, cancellationToken);

            if (chatRoom == null)
            {
                response.Success = false;
                response.StatusCode = 404;
                response.Message = "Chat room could not be found";
                return response;
            }

            // 2. Query the messages

            var messages = await _chatMessageRepository.GetAllChatMessagesForChatRoomAsync(request.ChatRoomId, cancellationToken);

            var allChatMessages = messages.Select(x => new GetAllChatMessagesQueryDto
            {
                ChatMessageId = x.ChatMessageId,
                Username = x.User.Username,
                AvatarUrl = x.User.AvatarUrl,
                Content = x.Content
            }).ToList();

            response.Success = true;
            response.Message = "Successfully queried all chat messages";
            response.ChatMessages = allChatMessages;
            return response;
            
        }
    }
}
