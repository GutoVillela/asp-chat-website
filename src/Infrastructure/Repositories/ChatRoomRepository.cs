using Domain.Entities;
using Domain.Queriables;
using Domain.Repositories;
using Infrastructure.Persistence.DataContext;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class ChatRoomRepository : Repository<ChatRoom>, IChatRoomRepository
    {
        public ChatRoomRepository(ApplicationDbContext dbContext) : base(dbContext) { }

        public async Task<IEnumerable<ChatRoom>> ReadAllByUserIncludingUsersAsync(string userId)
        {
            return await _dbSet.AsNoTracking()
                .Include(ChatRoomQueriable.IncludeUsers())
                .Where(ChatRoomQueriable.GetByUserId(userId))
                .ToListAsync();
        }

        public async Task<IEnumerable<ChatRoom>> ReadAllIncludingUsersAsync()
        {
            return await _dbSet.AsNoTracking()
                .Include(ChatRoomQueriable.IncludeUsers())
                .ToListAsync();
        }
    }
}
