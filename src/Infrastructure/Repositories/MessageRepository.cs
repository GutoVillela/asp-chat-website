using Domain.Entities;
using Domain.Queriables;
using Domain.Repositories;
using Infrastructure.Persistence.DataContext;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;

namespace Infrastructure.Repositories
{
    public class MessageRepository : Repository<Message>, IMessageRepository
    {
        public MessageRepository(ApplicationDbContext dbContext) : base(dbContext) { }

        public async Task<IEnumerable<Message>> ReadAllByChatIncludingAuthorAsync(int chatRoomId, int messagesToGet)
        {
            return await _dbSet.AsNoTracking()
                .Include(MessageQueriable.IncludeAuthor())
                .Where(MessageQueriable.GetByChatRoomId(chatRoomId))
                .OrderBy(MessageQueriable.CreationDate())
                .Take(messagesToGet)
                .ToListAsync();
        }
    }
}
