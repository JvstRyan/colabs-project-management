using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Colabs.ProjectManagement.Domain.Entities.TaskManagement;

namespace Colabs.ProjectManagement.Application.Contracts.Persistence
{
    public interface ISprintRepository : IGenericRepository<Sprint>
    {
        Task<Sprint> CreateSprintAsync(Guid workspaceId, Sprint sprint);
    }
}
