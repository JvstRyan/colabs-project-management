using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Colabs.ProjectManagement.Domain.Common;

namespace Colabs.ProjectManagement.Domain.Entities.TaskManagement
{
    public class TaskComment : AuditableEntity
    {
        public string TaskCommentId { get; set; } = string.Empty;
        public string TaskId { get; set; } = string.Empty;
        public string UserId { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;

        //Navigation properties
        public TaskEntity Task { get; private set; } = null!;
        public User User { get; private set; } = null!;
    }
}
