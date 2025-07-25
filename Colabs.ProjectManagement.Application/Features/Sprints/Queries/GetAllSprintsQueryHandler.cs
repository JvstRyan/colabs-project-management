using Colabs.ProjectManagement.Application.Contracts.Persistence;
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
            var response = new GetAllSprintsQueryResponse();

            try
            {
                var validator = new GetAllSprintsQueryValidator();
                var validationResult = await validator.ValidateAsync(request, cancellationToken);

                if (validationResult != null)
                {
                    response.Success = false;
                    response.StatusCode = 400;
                    response.ValidationErrors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                    return response;
                }

                // 1. Check if workspace exists

                var workspace = await _workspaceRepository.GetByIdAsync(request.WorkspaceId);

                if (workspace == null)
                {
                    response.Success = false;
                    response.StatusCode = 404;
                    response.Message = "Workspace could not be found";
                    return response;
                }

                // 2. Query sprints
                var sprints = await _sprintRepository.GetAllSprintsQueryAsync(request.WorkspaceId, cancellationToken);

                response.Success = true;
                response.Message = "Successfully retrieved sprints";
                response.Sprints = sprints;
                return response;

            }
            catch (Exception ex)
            {
                response.Success = false;
                response.StatusCode = 500;
                response.Message = $"Something went wrong while quering sprints: {ex.Message}";
                return response;
            }
        }
    }
}
