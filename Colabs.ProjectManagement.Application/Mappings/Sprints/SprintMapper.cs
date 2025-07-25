using Colabs.ProjectManagement.Application.Features.Sprints.Commands;
using Colabs.ProjectManagement.Domain.Entities.TaskManagement;
using Colabs.ProjectManagement.Domain.Enums;

namespace Colabs.ProjectManagement.Application.Mappings.Sprints
{
    public static class SprintMapper
    {
        public static Sprint ToSprint(this CreateSprintCommand request)
        {
            return new Sprint
            {
                SprintId = Guid.NewGuid().ToString(),
                WorkspaceId = request.WorkspaceId,
                Name = request.Name,
                Description = request?.Description,
                StartDate = request?.StartDate,
                EndDate = request?.EndDate,
                Status = SprintStatus.Planning
            };
        }

        public static CreateSprintCommand ToSprintCommand(this CreateSprintDto request, string workspaceId)
        {
            return new CreateSprintCommand
            {
                WorkspaceId = workspaceId,
                Name = request.Name,
                Description = request.Description,
                StartDate = request.StartDate,
                EndDate = request.EndDate
            };
        }

    }
}
