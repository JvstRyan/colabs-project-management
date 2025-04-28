using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Colabs.ProjectManagement.Domain.Common;
using Colabs.ProjectManagement.Domain.Entities.Chat;
using Colabs.ProjectManagement.Domain.Entities.Documents;
using Colabs.ProjectManagement.Domain.Entities.TaskManagement;
using Colabs.ProjectManagement.Domain.Entities.Workspaces;

namespace Colabs.ProjectManagement.Domain.Entities
{
    public class User : AuditableEntity
    {   
        public Guid UserId { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PasswordHash {get; set; } 
        public string AvatarUrl { get; set; } = string.Empty;

        public ICollection<Workspace> OwnedWorkspaces { get; private set; } = new List<Workspace>();
        public ICollection<WorkspaceMember> WorkspaceMemberships { get; private set; } = new List<WorkspaceMember>();
        public ICollection<TaskEntity> CreatedTasks { get; private set; } = new List<TaskEntity>();
        public ICollection<TaskEntity> AssignedTasks { get; private set; } = new List<TaskEntity>();
        public ICollection<TaskComment> TaskComments { get; private set; } = new List<TaskComment>();
        public ICollection<ChatMessage> ChatMessages { get; private set; } = new List<ChatMessage>();
        public ICollection<Document> CreatedDocuments { get; private set; } = new List<Document>();
        public ICollection<WorkspaceInvitation> SentInvitations { get; private set; } = new List<WorkspaceInvitation>();
        public ICollection<WorkspaceInvitation> ReceivedInvitations { get; private set; } = new List<WorkspaceInvitation>();

    }
}
