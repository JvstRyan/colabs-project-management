using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colabs.ProjectManagement.Domain.Common
{
    public class AuditableEntity
    {
        public Guid? CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; } 
        public Guid? LastModifiedBy { get; set; }
        public DateTime? LastModifiedDate { get; set; }
    }
}
