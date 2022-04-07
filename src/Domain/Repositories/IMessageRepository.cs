using Domain.Entities;
using Shared.Repositories;

namespace Domain.Repositories
{
    public interface IMessageRepository : IRepository<Message>
    {
        Task<IEnumerable<Message>> ReadAllByChatIncludingAuthorAsync(int chatId, int messagesToGet);
        
    }
}
