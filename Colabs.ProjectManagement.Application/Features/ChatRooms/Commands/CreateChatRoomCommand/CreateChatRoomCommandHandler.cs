using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Colabs.ProjectManagement.Application.Contracts.Persistence;
using Colabs.ProjectManagement.Domain.Entities.Chat;
using Colabs.ProjectManagement.Domain.Entities.Workspaces;
using MediatR;

namespace Colabs.ProjectManagement.Application.Features.ChatRooms.Commands.CreateChatRoomCommand
{
    public class CreateChatRoomCommandHandler : IRequestHandler<CreateChatRoomCommand, CreateChatRoomCommandResponse>
    {
        private readonly IGenericRepository<ChatRoom> _chatRoomRepository;
        private readonly IGenericRepository<Workspace> _workspaceRepository;

        public CreateChatRoomCommandHandler(IGenericRepository<ChatRoom> chatRoomRepository, IGenericRepository<Workspace> workspaceRepository)
        {
            _chatRoomRepository = chatRoomRepository;
            _workspaceRepository = workspaceRepository;
        }

        public async Task<CreateChatRoomCommandResponse> Handle(CreateChatRoomCommand request, CancellationToken cancellationToken)
        {
            // 1. Check if workspace exists
            var workspace = await _workspaceRepository.GetByIdAsync(request.WorkspaceId, cancellationToken);

            if (workspace is null)
                throw new NotFoundException("Workspace", request.WorkspaceId);

            var chatRoom = new ChatRoom
            {
                ChatRoomId = Guid.NewGuid().ToString(),
                WorkspaceId = request.WorkspaceId,
                Name = request.Name
            };

            await _chatRoomRepository.AddAsync(chatRoom, cancellationToken);

            return new CreateChatRoomCommandResponse(true);
        }
    }
}