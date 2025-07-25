using Colabs.ProjectManagement.Application.Contracts.Persistence;
using Colabs.ProjectManagement.Application.Features.Sprints.Queries;
using Colabs.ProjectManagement.Domain.Entities.TaskManagement;
using Microsoft.EntityFrameworkCore;

namespace Colabs.ProjectManagement.Persistence.Repositories
{
    public class SprintRepository : BaseRepository<Sprint>, ISprintRepository
    {

        public SprintRepository(ColabsDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<IReadOnlyList<GetAllSprintsQueryDto>> GetAllSprintsQueryAsync(string workspaceId, CancellationToken cancellationToken = default)
        {
            return await _dbContext.Sprints
                .AsNoTracking()
                .Where(x => x.WorkspaceId == workspaceId)
                .Select(s => new GetAllSprintsQueryDto
                {
                    SprintId = s.SprintId,
                    Name = s.Name,
                    Description = s.Description,
                    StartDate = s.StartDate,
                    EndDate = s.EndDate
                })
                .ToListAsync(cancellationToken);
        }
    }
}
