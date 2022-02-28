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

        public string DecryptMessage(string messageHash)
        {
            // TODO: ATTENTION: This method isn't really decypting the message
            return messageHash;
        }

        public string EncryptMessage(string messageText)
        {
            // TODO: ATTENTION: This method isn't really encypting the message
            return messageText;
        }

        public async Task<IEnumerable<Message>> ReadAllByChatIncludingAuthorAsync(int chatRoomId)
        {
            return await _dbSet.AsNoTracking()
                .Include(MessageQueriable.IncludeAuthor())
                .Where(MessageQueriable.GetByChatRoomId(chatRoomId))
                .ToListAsync();
        }
    }
}
