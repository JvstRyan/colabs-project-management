using Colabs.ProjectManagement.Application.Contracts.Persistence;
using Colabs.ProjectManagement.Application.Exceptions;
using Colabs.ProjectManagement.Domain.Entities.Workspaces;
using MediatR;

namespace Colabs.ProjectManagement.Application.Features.Sprints.Queries
{
    public class GetAllSprintsQueryHandler : IRequestHandler<GetAllSprintsQuery, GetAllSprintsQueryResponse>
    {
        private readonly ISprintRepository _sprintRepository;
        private readonly IGenericRepository<Workspace> _workspaceRepository;

        public GetAllSprintsQueryHandler(ISprintRepository sprintRepository, IGenericRepository<Workspace> workspaceRepository)
        {
            _sprintRepository = sprintRepository;
            _workspaceRepository = workspaceRepository;
        }

        public async Task<GetAllSprintsQueryResponse> Handle(GetAllSprintsQuery request, CancellationToken cancellationToken)
        {
            

            // 1. Check if workspace exists

            var workspace = await _workspaceRepository.GetByIdAsync(request.WorkspaceId);

            if (workspace is null)
                throw new NotFoundException("Workspace", request.WorkspaceId);

            // 2. Query sprints
            var sprints = await _sprintRepository.GetAllSprintsQueryAsync(request.WorkspaceId, cancellationToken);


            return new GetAllSprintsQueryResponse(sprints);
     
        }
    }
}
