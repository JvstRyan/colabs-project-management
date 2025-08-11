using Colabs.ProjectManagement.Domain.Enums;
using MediatR;

namespace Colabs.ProjectManagement.Application.Features.Sprints.Commands
{
    public class CreateSprintCommand : IRequest<CreateSprintCommandResult>
    {
        public string WorkspaceId {get; set;} = string.Empty;
        public string Name {get; set;} = string.Empty;
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate {get; set;}
        public string? Description {get; set;} = string.Empty;
    }
}
