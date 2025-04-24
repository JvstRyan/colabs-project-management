using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Colabs.ProjectManagement.Domain.Common;

namespace Colabs.ProjectManagement.Domain.Entities
{
    public class Workspace : AuditableEntity
    {
        public int Id { get; set; }
        public string Name { get; set; } 
        public string Description { get; set; }
        public int OwnerId { get; set; }
        public string BannerUrl { get; set; }

        //Navigation properties



    }
}
