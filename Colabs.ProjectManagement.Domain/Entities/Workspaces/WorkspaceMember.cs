using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colabs.ProjectManagement.Domain.Entities.Workspaces
{
    public class WorkspaceMember
    {
        public Guid WorkspaceId { get; set; }
        public Guid UserId { get; set; }
        public Guid RoleId { get; set; }
        public DateTime JoinedAt { get; set; }

        //Navigation properties
        public Workspace Workspace { get; private set; } = null!;
        public User User { get; private set; } = null!;
        public Role Role { get; private set; } = null!;
    }
}
