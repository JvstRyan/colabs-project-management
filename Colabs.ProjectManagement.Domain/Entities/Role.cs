using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Colabs.ProjectManagement.Domain.Entities.Workspaces;

namespace Colabs.ProjectManagement.Domain.Entities
{
    public class Role 
    {
        public Guid RoleId { get; set; }
        public Guid WorkspaceId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public bool CanManageUsers { get; set; }
        public bool CanManageTasks { get; set; }
        public bool CanManageSprints { get; set; }
        public bool CanManageChatrooms { get; set; }
        public bool CanManageDocs { get; set; }
        public bool CanCreateDocs { get; set; }
        public bool CanEditDocs { get; set; }
        public bool CanReadDocs { get; set; }

        public Workspace Workspace { get; private set; } = null!;
        public ICollection<WorkspaceMember> Members { get; private set; } = new List<WorkspaceMember>();

    }
}
