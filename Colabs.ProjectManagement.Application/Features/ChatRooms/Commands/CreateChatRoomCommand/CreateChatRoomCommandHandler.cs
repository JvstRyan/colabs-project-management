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
            var response = new CreateChatRoomCommandResponse();

            try
            {
                var validator = new CreateChatRoomCommandValidator();
                var validationResult = await validator.ValidateAsync(request, cancellationToken);

                if (!validationResult.IsValid)
                {
                    response.Success = false;
                    response.StatusCode = 400;
                    response.ValidationErrors = validationResult.Errors.Select(x => x.ErrorMessage).ToList();
                    return response;
                }

                // 1. Check if workspace exists
                var workspace = await _workspaceRepository.GetByIdAsync(request.WorkspaceId, cancellationToken);

                if (workspace == null)
                {
                    response.Success = false;
                    response.Message = "Workspace could not be found";
                    response.StatusCode = 404;
                    return response;
                }

                var chatRoom = new ChatRoom
                {
                    ChatRoomId = Guid.NewGuid().ToString(),
                    WorkspaceId = request.WorkspaceId,
                    Name = request.Name
                };

                await _chatRoomRepository.AddAsync(chatRoom, cancellationToken);

                response.Success = true;
                response.Message = "Chat room has successfully been created";
                return response;
                
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.StatusCode = 500;
                response.Message = $"Something went wrong: {ex.Message} ";
                return response;
            }
        }
    }
}
