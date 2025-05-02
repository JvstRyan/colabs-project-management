using Colabs.ProjectManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Colabs.ProjectManagement.Persistence.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(u => u.UserId);
            
            // Configuration for workspace relations
            builder.HasMany(u => u.OwnedWorkspaces)
                .WithOne(w => w.Owner)
                .HasForeignKey(w => w.OwnerId)
                .OnDelete(DeleteBehavior.Restrict);
            
            // Configuration for created tasks
            builder.HasMany(u => u.CreatedTasks)
                .WithOne(t => t.Creator)
                .HasForeignKey(t => t.CreatorId)
                .OnDelete(DeleteBehavior.Restrict);
            
            // Configure relationships with assigned tasks
            builder.HasMany(u => u.AssignedTasks)
                .WithOne(t => t.Assignee)
                .HasForeignKey(t => t.AssigneeId)
                .OnDelete(DeleteBehavior.Restrict);
            
            // Configure relationships with created documents
            builder.HasMany(u => u.CreatedDocuments)
                .WithOne(d => d.CreatedBy)
                .HasForeignKey(d => d.CreatedByUserId)
                .OnDelete(DeleteBehavior.Restrict);
            
            // Configure relationships with sent invitations
            builder.HasMany(u => u.SentInvitations)
                .WithOne(i => i.Inviter)
                .HasForeignKey(i => i.InviterId)
                .OnDelete(DeleteBehavior.Restrict);
            
            // Configure relationships with received invitations
            builder.HasMany(u => u.ReceivedInvitations)
                .WithOne(i => i.Invitee)
                .HasForeignKey(i => i.InviteeId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
