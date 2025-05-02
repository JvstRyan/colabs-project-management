using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Colabs.ProjectManagement.Domain.Common;
using Colabs.ProjectManagement.Domain.Entities.Workspaces;

namespace Colabs.ProjectManagement.Domain.Entities.Documents
{
    public class DocumentFolder : AuditableEntity
    {
        public Guid DocumentFolderId { get; set; }
        public Guid WorkspaceId { get; set; }
        public Guid? ParentFolderId { get; set; }
        public string Name { get; set; } = string.Empty;

        //Navigation properties 
        public Workspace Workspace { get; private set; } = null!;
        public DocumentFolder? ParentFolder { get; private set; }
        public ICollection<DocumentFolder> SubFolders { get; private set; } = new List<DocumentFolder>();
        public ICollection<Document> Documents { get; private set; } = new List<Document>();

    }
}
