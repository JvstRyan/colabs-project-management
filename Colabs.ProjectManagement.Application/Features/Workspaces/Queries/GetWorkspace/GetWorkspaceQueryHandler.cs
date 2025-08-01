﻿using Colabs.ProjectManagement.Application.Contracts.Persistence;
using Colabs.ProjectManagement.Application.Mappings;
using Colabs.ProjectManagement.Domain.Entities.Workspaces;
using MediatR;



namespace Colabs.ProjectManagement.Application.Features.Workspaces.Queries.GetWorkspace
{

    public class GetWorkspaceQueryHandler : IRequestHandler<GetWorkspaceQuery, GetWorkspaceQueryResponse>
    {
        private readonly IGenericRepository<Workspace> _workspaceRepository;

        public GetWorkspaceQueryHandler(IGenericRepository<Workspace> workspaceRepository)
        {
            _workspaceRepository = workspaceRepository;
        }
        
        public async Task<GetWorkspaceQueryResponse> Handle(GetWorkspaceQuery request, CancellationToken cancellationToken)
        {
            var workspace = await _workspaceRepository.GetByIdAsync(request.WorkspaceId, cancellationToken);

            if (workspace == null)
            {
                return new GetWorkspaceQueryResponse
                {
                    Success = false,
                    StatusCode = 404,
                    Message = "Workspace could not be found"
                };
            }
            
            return new GetWorkspaceQueryResponse
            {
                Success = true,
                Message = "Retrieved workspace successfully",
                Workspace = workspace.ToWorkspaceDto()
            };
            
        }
    }
}
