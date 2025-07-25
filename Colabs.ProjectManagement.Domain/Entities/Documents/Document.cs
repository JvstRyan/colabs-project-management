using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Colabs.ProjectManagement.Domain.Common;
using Colabs.ProjectManagement.Domain.Entities.Workspaces;

namespace Colabs.ProjectManagement.Domain.Entities.Documents
{
    public class Document : AuditableEntity
    {
        public string DocumentId { get; set; } = string.Empty;
        public string? DocumentFolderId { get; set; } 
        public string WorkspaceId { get; set; }  = string.Empty;
        public string CreatedByUserId { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;

        //Navigation properties
        public Workspace Workspace { get; private set; } = null!;
        public DocumentFolder? DocumentFolder { get; private set; } 
        public new User CreatedBy {get; private set;} = null!;

    }
}
