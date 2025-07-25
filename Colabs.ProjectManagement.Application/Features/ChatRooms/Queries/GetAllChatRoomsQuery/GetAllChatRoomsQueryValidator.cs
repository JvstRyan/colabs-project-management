using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace Colabs.ProjectManagement.Application.Features.ChatRooms.Queries.GetAllChatRoomsQuery
{
    public class GetAllChatRoomsQueryValidator : AbstractValidator<GetAllChatRoomsQuery>
    {
        public GetAllChatRoomsQueryValidator()
        {
            RuleFor(x => x.WorkspaceId)
                .NotEmpty().WithMessage("Please provide the workspace id");
        }
    }
}
