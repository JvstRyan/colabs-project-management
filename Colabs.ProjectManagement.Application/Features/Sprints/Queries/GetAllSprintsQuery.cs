using MediatR;

namespace Colabs.ProjectManagement.Application.Features.Sprints.Queries
{
    public class GetAllSprintsQuery : IRequest<GetAllSprintsQueryResponse>
    {
        public string WorkspaceId {get; set;} = string.Empty;
    }
}
