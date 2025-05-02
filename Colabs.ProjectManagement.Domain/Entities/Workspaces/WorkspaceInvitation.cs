using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Colabs.ProjectManagement.Domain.Common;
using Colabs.ProjectManagement.Domain.Enums;

namespace Colabs.ProjectManagement.Domain.Entities.Workspaces
{
    public class WorkspaceInvitation : AuditableEntity
    {
        public Guid WorkspaceInvitationId { get; set; }
        public Guid WorkspaceId { get; set; }
        public Guid InviterId { get; set; }
        public Guid InviteeId { get; set; }
        public InvitationStatus Status { get; set; }

        //Navigation properties
        public Workspace Workspace { get; private set; } = null!;
        public User Inviter { get; private set; } = null!;
        public User Invitee { get; private set; } = null!;
    }
}
