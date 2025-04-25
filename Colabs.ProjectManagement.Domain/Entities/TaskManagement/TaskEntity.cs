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
    public class TaskEntity : AuditableEntity
    {
        public Guid TaskId { get; set; }
        public Guid WorkspaceId { get; set; }
        public Guid? SprintId { get; set; }
        public Guid CreatorId { get; set; }
        public Guid? AssigneeId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public TaskType TaskType { get; set; }
        public TaskStatusType Status { get; set; }
        public TaskPriority Priority { get; set; }
        public DateTime? Deadline { get; set; }

        //Navigation properties
        public Workspace Workspace { get; private set; } = null!;
        public Sprint? Sprint { get; private set; }
        public User Creator { get; private set; } = null!;
        public User? Assignee { get; private set; }
        public ICollection<TaskComment> Comments { get; private set; } = new List<TaskComment>();
    }
}
