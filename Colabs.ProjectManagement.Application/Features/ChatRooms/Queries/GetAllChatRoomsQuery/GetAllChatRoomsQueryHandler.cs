using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Colabs.ProjectManagement.Application.Contracts.Persistence;
using MediatR;

namespace Colabs.ProjectManagement.Application.Features.ChatRooms.Queries.GetAllChatRoomsQuery
{
    public class GetAllChatRoomsQueryHandler : IRequestHandler<GetAllChatRoomsQuery, GetAllChatRoomsQueryResponse>
    {
        private readonly IChatRoomRepository _chatRoomRepository;

        public GetAllChatRoomsQueryHandler(IChatRoomRepository chatRoomRepository)
        {
            _chatRoomRepository = chatRoomRepository;
        }
        public async Task<GetAllChatRoomsQueryResponse> Handle(GetAllChatRoomsQuery request, CancellationToken cancellationToken)
        {
            var response = new GetAllChatRoomsQueryResponse();

           try
            {
                var validator = new GetAllChatRoomsQueryValidator();
                var validationResult = await validator.ValidateAsync(request, cancellationToken);

                if (!validationResult.IsValid)
                {
                    response.Success = false;
                    response.StatusCode = 400;
                    response.ValidationErrors = validationResult.Errors.Select(x => x.ErrorMessage).ToList();
                    response.Message = "Something went wrong while validating";
                    return response;
                }

                var chatRooms = await _chatRoomRepository.GetAllChatRoomsByWorkspaceId(request.WorkspaceId, cancellationToken);

                var allChatRooms = chatRooms.Select(x => new GetAllChatRoomsDto
                {
                    ChatRoomId = x.ChatRoomId,
                    Name = x.Name,

                }).ToList();

                response.Success = true;
                response.ChatRooms = allChatRooms;
                return response;

            }
            catch (Exception ex)
            {
                response.Success = false;
                response.StatusCode = 500;
                response.Message = $"Something went wrong {ex.Message}";
                return response;
            }
        }
    }
}
