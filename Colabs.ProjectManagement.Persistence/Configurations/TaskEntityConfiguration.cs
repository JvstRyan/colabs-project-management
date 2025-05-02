using Colabs.ProjectManagement.Domain.Entities.TaskManagement;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Colabs.ProjectManagement.Persistence.Configurations
{
    public class TaskEntityConfiguration : IEntityTypeConfiguration<TaskEntity>
    {

        public void Configure(EntityTypeBuilder<TaskEntity> builder)
        {
           builder.HasKey(t => t.TaskId);
           
           builder.ToTable("Tasks");
           
           // Relationship with workspace
           builder.HasOne(t => t.Workspace)
               .WithMany(w => w.Tasks)
               .HasForeignKey(t => t.WorkspaceId)
               .OnDelete(DeleteBehavior.Cascade);
           
           // Relationship with sprint (optional)
           builder.HasOne(t => t.Sprint)
               .WithMany(s => s.Tasks)
               .HasForeignKey(t => t.SprintId)
               .IsRequired(false)  
               .OnDelete(DeleteBehavior.SetNull);
           
           // Relationship with creator
           builder.HasOne(t => t.Creator)
               .WithMany(u => u.CreatedTasks)
               .HasForeignKey(t => t.CreatorId)
               .OnDelete(DeleteBehavior.Restrict);
           
           
           // Relationship with assignee (optional)
           builder.HasOne(t => t.Assignee)
               .WithMany(u => u.AssignedTasks)
               .HasForeignKey(t => t.AssigneeId)
               .IsRequired(false)  
               .OnDelete(DeleteBehavior.SetNull);
           
           // Relationship with comments
           builder.HasMany(t => t.Comments)
               .WithOne(c => c.Task)
               .HasForeignKey(c => c.TaskId)
               .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
