using Domain.Entities;
using Domain.Repositories;
using Infrastructure.Persistence.DataContext;

namespace Infrastructure.Repositories
{
    public class ChatRoomRepository : Repository<ChatRoom>, IChatRoomRepository
    {
        public ChatRoomRepository(ApplicationDbContext dbContext) : base(dbContext) { }
    }
}
