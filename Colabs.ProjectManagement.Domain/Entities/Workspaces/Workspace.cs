using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Colabs.ProjectManagement.Domain.Common;
using Colabs.ProjectManagement.Domain.Entities.Chat;
using Colabs.ProjectManagement.Domain.Entities.Documents;
using Colabs.ProjectManagement.Domain.Entities.TaskManagement;

namespace Colabs.ProjectManagement.Domain.Entities.Workspaces
{
    public class Workspace : AuditableEntity
    {
        public string WorkspaceId { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; } = string.Empty;
        public string OwnerId { get; set; } = string.Empty;
        public string? ProfileUrl {get; set;} = string.Empty;
        public string? BannerUrl { get; set; } = string.Empty;

        // Navigation properties
        public User Owner { get; private set; } = null!;
        public ICollection<WorkspaceMember> Members { get; private set; } = new List<WorkspaceMember>();
        public ICollection<Role> Roles { get; private set; } = new List<Role>();
        public ICollection<Sprint> Sprints { get; private set; } = new List<Sprint>();
        public ICollection<TaskEntity> Tasks { get; private set; } = new List<TaskEntity>();
        public ICollection<ChatRoom> ChatRooms { get; private set; } = new List<ChatRoom>();
        public ICollection<DocumentFolder> DocumentFolders { get; private set; } = new List<DocumentFolder>();
        public ICollection<Document> Documents { get; private set; } = new List<Document>();
        public ICollection<WorkspaceInvitation> Invitations { get; private set; } = new List<WorkspaceInvitation>();
        
    }
}
