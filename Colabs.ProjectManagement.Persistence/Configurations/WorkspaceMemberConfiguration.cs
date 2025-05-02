using Colabs.ProjectManagement.Domain.Entities.Workspaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Colabs.ProjectManagement.Persistence.Configurations
{
    public class WorkspaceMemberConfiguration : IEntityTypeConfiguration<WorkspaceMember> 
    {

        public void Configure(EntityTypeBuilder<WorkspaceMember> builder)
        { 
            // Configure composite primary key
          builder.HasKey(wm => new {wm.WorkspaceId, wm.UserId});
          
          // Configure relationship with workspace
          builder.HasOne(wm => wm.Workspace)
              .WithMany(w => w.Members)
              .HasForeignKey(wm => wm.WorkspaceId)
              .OnDelete(DeleteBehavior.Cascade);
          
          // Configure relationship with user
          builder.HasOne(wm => wm.User)
              .WithMany(u => u.WorkspaceMemberships)
              .HasForeignKey(wm => wm.UserId)
              .OnDelete(DeleteBehavior.Restrict);
          
          // Configure relationship with role
          builder.HasOne(wm => wm.Role)
              .WithMany(r => r.Members)
              .HasForeignKey(wm => wm.RoleId)
              .OnDelete(DeleteBehavior.Restrict);
          
        }
    }
}
