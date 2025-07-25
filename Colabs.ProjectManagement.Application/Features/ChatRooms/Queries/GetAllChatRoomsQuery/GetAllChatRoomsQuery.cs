using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace Colabs.ProjectManagement.Application.Features.ChatRooms.Queries.GetAllChatRoomsQuery
{
    public class GetAllChatRoomsQuery : IRequest<GetAllChatRoomsQueryResponse>
    {
        public string WorkspaceId { get; set; } = string.Empty;
    }
}
