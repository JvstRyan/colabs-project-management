using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Colabs.ProjectManagement.Domain.Entities.TaskManagement;

namespace Colabs.ProjectManagement.Application.Contracts.Persistence
{
    public interface ITaskRepository : IGenericRepository<TaskEntity>
    {
        Task<TaskEntity> CreateTaskAsync(Guid workspaceId, Guid? sprintId, TaskEntity task);
    }
}
