using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Map
{
    internal class MessageMap : IEntityTypeConfiguration<Message>
    {
        public void Configure(EntityTypeBuilder<Message> builder)
        {
            builder.HasOne(x => x.Author).WithMany(x => x.Messages).HasForeignKey(x => x.AuthorId);
            builder.HasOne(x => x.ChatRoom).WithMany(x => x.Messages).HasForeignKey(x => x.ChatRoomId);
            builder.Ignore(x => x.Notifications);
            builder.Ignore(x => x.IsValid);
        }
    }
}
