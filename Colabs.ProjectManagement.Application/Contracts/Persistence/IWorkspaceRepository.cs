using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Colabs.ProjectManagement.Domain.Entities.Workspaces;

namespace Colabs.ProjectManagement.Application.Contracts.Persistence
{
    public interface IWorkspaceRepository : IGenericRepository<Workspace>
    {
    }
}
