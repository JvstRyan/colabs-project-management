using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colabs.ProjectManagement.Domain.Entities.Workspaces
{
    public class WorkspaceMember
    {
        public string WorkspaceId { get; set; } = string.Empty;
        public string UserId { get; set; } = string.Empty;
        public string RoleId { get; set; } = string.Empty;
        public DateTime JoinedAt { get; set; }

        //Navigation properties
        public Workspace Workspace { get; private set; } = null!;
        public User User { get; private set; } = null!;
        public Role Role { get; private set; } = null!;
    }
}
 