
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Map
{
    internal class ChatRoomMap : IEntityTypeConfiguration<ChatRoom>
    {
        public void Configure(EntityTypeBuilder<ChatRoom> builder)
        {
            builder.HasMany(x => x.Users).WithMany(x => x.ChatRooms);
            builder.HasMany(x => x.Messages).WithOne(x => x.ChatRoom).HasForeignKey(x => x.ChatRoomId);
            builder.Ignore(x => x.Notifications);
            builder.Ignore(x => x.IsValid);
        }
    }
}
