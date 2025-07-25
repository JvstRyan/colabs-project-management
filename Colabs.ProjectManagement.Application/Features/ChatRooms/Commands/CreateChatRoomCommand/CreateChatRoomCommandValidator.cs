using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace Colabs.ProjectManagement.Application.Features.ChatRooms.Commands.CreateChatRoomCommand
{
    public class CreateChatRoomCommandValidator : AbstractValidator<CreateChatRoomCommand>
    {
        public CreateChatRoomCommandValidator()
        {
            RuleFor(x => x.WorkspaceId)
                .NotEmpty().WithMessage("Please provide the workspace id to create chat room");

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Please provide a name for the chat room")
                .MaximumLength(50).WithMessage("Chat room name cannot contain more than 50 charachters");
        }
    }
}
