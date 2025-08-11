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
            var chatRooms = await _chatRoomRepository.GetAllChatRoomsByWorkspaceId(request.WorkspaceId, cancellationToken);

            var allChatRooms = chatRooms.Select(x => new GetAllChatRoomsDto
            {
                ChatRoomId = x.ChatRoomId,
                Name = x.Name,
            }).ToList();

            return new GetAllChatRoomsQueryResponse(allChatRooms);
        }
    }
}
