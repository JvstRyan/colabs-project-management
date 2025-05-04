using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Colabs.ProjectManagement.Domain.Common;
using Colabs.ProjectManagement.Domain.Entities.Workspaces;
using Colabs.ProjectManagement.Domain.Enums;

namespace Colabs.ProjectManagement.Domain.Entities.TaskManagement
{
    public class Sprint : AuditableEntity
    {
        public string SprintId { get; set; } = string.Empty;
        public string WorkspaceId { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Description { get; set; } = string.Empty;
        public SprintStatus Status { get; set; }

        //Navigation properties
        public Workspace Workspace { get; private set; } = null!;
        public ICollection<TaskEntity> Tasks { get; private set; } = new List<TaskEntity>();
    }
}
