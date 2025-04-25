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
        public Guid TaskCommentId { get; set; }
        public Guid TaskId { get; set; }
        public Guid UserId { get; set; }
        public string Content { get; set; }

        //Navigation properties
        public TaskEntity Task { get; private set; } = null!;
        public User User { get; private set; } = null!;
    }
}
