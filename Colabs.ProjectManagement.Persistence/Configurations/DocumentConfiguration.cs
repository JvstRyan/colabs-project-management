using Colabs.ProjectManagement.Domain.Entities.Documents;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Colabs.ProjectManagement.Persistence.Configurations
{
    public class DocumentConfiguration : IEntityTypeConfiguration<Document>
    {

        public void Configure(EntityTypeBuilder<Document> builder)
        {
            builder.HasKey(d => d.DocumentId);
            
            // Configure relationship with workspace
            builder.HasOne(d => d.Workspace)
                .WithMany(w => w.Documents)
                .HasForeignKey(d => d.WorkspaceId)
                .OnDelete(DeleteBehavior.Cascade);

            // Configure relationship with document folder
            builder.HasOne(d => d.DocumentFolder)
                .WithMany(f => f.Documents)
                .HasForeignKey(d => d.DocumentFolderId)
                .OnDelete(DeleteBehavior.SetNull);

            // Configure relationship with creator
            builder.HasOne(d => d.CreatedBy)
                .WithMany(u => u.CreatedDocuments)
                .HasForeignKey(d => d.CreatedByUserId)
                .OnDelete(DeleteBehavior.Restrict);
            
        }
    }
}
