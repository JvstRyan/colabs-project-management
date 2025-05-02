using Colabs.ProjectManagement.Domain.Entities.Workspaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Colabs.ProjectManagement.Persistence.Configurations
{
    public class WorkspaceConfiguration : IEntityTypeConfiguration<Workspace>
    {

        public void Configure(EntityTypeBuilder<Workspace> builder)
        {
           builder.HasKey(w => w.WorkspaceId);
           
           // Configure relationship for owner
           builder.HasOne(w => w.Owner)
               .WithMany(u => u.OwnedWorkspaces)
               .HasForeignKey(w => w.OwnerId)
               .OnDelete(DeleteBehavior.Restrict);
           
           // Configure relationship with members
           builder.HasMany(w => w.Members)
               .WithOne(m => m.Workspace)
               .HasForeignKey(m => m.WorkspaceId)
               .OnDelete(DeleteBehavior.Cascade);
           
           
           // Relationship with roles
           builder.HasMany(w => w.Roles)
               .WithOne(r => r.Workspace)
               .HasForeignKey(r => r.WorkspaceId)
               .OnDelete(DeleteBehavior.Cascade);
           
           // Relationship with sprints
           builder.HasMany(w => w.Sprints)
               .WithOne(s => s.Workspace)
               .HasForeignKey(s => s.WorkspaceId)
               .OnDelete(DeleteBehavior.Cascade);
           
           // Relationship with tasks
           builder.HasMany(w => w.Tasks)
               .WithOne(t => t.Workspace)
               .HasForeignKey(t => t.WorkspaceId)
               .OnDelete(DeleteBehavior.Cascade);
           
           // Relationship with chat rooms
           builder.HasMany(w => w.ChatRooms)
               .WithOne(c => c.Workspace)
               .HasForeignKey(c => c.WorkspaceId)
               .OnDelete(DeleteBehavior.Cascade);

           // Relationship with document folders
           builder.HasMany(w => w.DocumentFolders)
               .WithOne(df => df.Workspace)
               .HasForeignKey(df => df.WorkspaceId)
               .OnDelete(DeleteBehavior.Cascade);

           // Relationship with documents
           builder.HasMany(w => w.Documents)
               .WithOne(d => d.Workspace)
               .HasForeignKey(d => d.WorkspaceId)
               .OnDelete(DeleteBehavior.Cascade);

           // Relationship with invitations
           builder.HasMany(w => w.Invitations)
               .WithOne(i => i.Workspace)
               .HasForeignKey(i => i.WorkspaceId)
               .OnDelete(DeleteBehavior.Cascade);
          
        }
    }
}
