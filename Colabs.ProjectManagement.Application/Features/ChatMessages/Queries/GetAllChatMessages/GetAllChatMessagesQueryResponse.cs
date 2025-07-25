using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Colabs.ProjectManagement.Application.Responses;

namespace Colabs.ProjectManagement.Application.Features.ChatMessages.Queries.GetAllChatMessages
{
    public class GetAllChatMessagesQueryResponse : BaseResponse
    {
        public GetAllChatMessagesQueryResponse(): base() { }

        public IReadOnlyList<GetAllChatMessagesQueryDto> ChatMessages { get; set; }
    }
}
