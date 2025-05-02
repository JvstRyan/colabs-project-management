using Colabs.ProjectManagement.Domain.Entities.Workspaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Colabs.ProjectManagement.Persistence.Configurations
{
    public class WorkspaceInvitationConfiguration : IEntityTypeConfiguration<WorkspaceInvitation>
    {

        public void Configure(EntityTypeBuilder<WorkspaceInvitation> builder)
        {
           builder.HasKey(i => i.WorkspaceInvitationId);
           
           // Relationship with Workspace
           builder.HasOne(i => i.Workspace)
               .WithMany(w => w.Invitations)
               .HasForeignKey(i => i.WorkspaceId)
               .OnDelete(DeleteBehavior.Cascade);
           
           // Relationship with Inviter (User)
           builder.HasOne(i => i.Inviter)
               .WithMany(u => u.SentInvitations)
               .HasForeignKey(i => i.InviterId)
               .OnDelete(DeleteBehavior.Restrict);
           
           // Relationship with Invitee (User)
           builder.HasOne(i => i.Invitee)
               .WithMany(u => u.ReceivedInvitations)
               .HasForeignKey(i => i.InviteeId)
               .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
