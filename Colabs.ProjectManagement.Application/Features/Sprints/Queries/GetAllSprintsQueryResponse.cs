using Colabs.ProjectManagement.Application.Responses;

namespace Colabs.ProjectManagement.Application.Features.Sprints.Queries
{
    public record GetAllSprintsQueryResponse(IReadOnlyList<GetAllSprintsQueryDto> Sprints);
}
