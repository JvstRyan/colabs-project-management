using Colabs.ProjectManagement.Domain.Enums;

namespace Colabs.ProjectManagement.Application.Features.Sprints.Commands
{
    public class CreateSprintDto
    {
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; } = string.Empty;
        public DateTime? StartDate { get; set; } 
        public DateTime? EndDate { get; set; }
        public SprintStatus Status { get; set; }

    }
}
