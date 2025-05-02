using Colabs.ProjectManagement.Domain.Entities.Documents;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Colabs.ProjectManagement.Persistence.Configurations
{
    public class DocumentFolderConfiguration : IEntityTypeConfiguration<DocumentFolder>
    {

        public void Configure(EntityTypeBuilder<DocumentFolder> builder)
        {
           builder.HasKey(df => df.DocumentFolderId);
           
           // Configure relationship with workspace
           builder.HasOne(df => df.Workspace)
               .WithMany(w => w.DocumentFolders)
               .HasForeignKey(df => df.WorkspaceId)
               .OnDelete(DeleteBehavior.Cascade);
           
           // Configure self-referencing relationship for folder hierarchy
           builder.HasOne(df => df.ParentFolder)
               .WithMany(df => df.SubFolders)
               .HasForeignKey(df => df.ParentFolderId)
               .OnDelete(DeleteBehavior.Restrict);
           
           
           
        }
    }
}
