using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Colabs.ProjectManagement.Application.Responses;

namespace Colabs.ProjectManagement.Application.Features.ChatRooms.Queries.GetAllChatRoomsQuery
{
    public class GetAllChatRoomsQueryResponse : BaseResponse
    {
        public GetAllChatRoomsQueryResponse(): base() { }

        public IReadOnlyList<GetAllChatRoomsDto> ChatRooms { get; set; }
    }
}
