using Colabs.ProjectManagement.Domain.Entities.Chat;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Colabs.ProjectManagement.Persistence.Configurations
{
    public class ChatRoomMemberConfiguration : IEntityTypeConfiguration<ChatRoomMember>
    {

        public void Configure(EntityTypeBuilder<ChatRoomMember> builder)
        {
            // Configure composite primary key
            builder.HasKey(m => new {m.ChatRoomId, m.UserId});
            
            // Configure relationship with chat room
            builder.HasOne(m => m.ChatRoom)
                .WithMany(cr => cr.Members)
                .HasForeignKey(m => m.ChatRoomId)
                .OnDelete(DeleteBehavior.Cascade);
            
            // Configure relationship with user
            builder.HasOne(m => m.User)
                .WithMany()
                .HasForeignKey(m => m.UserId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
