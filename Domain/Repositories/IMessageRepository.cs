using Domain.Entities;
using Shared.Repositories;

namespace Domain.Repositories
{
    public interface IMessageRepository : IRepository<Message>
    {
        string EncryptMessage(string messageText);
        string DecryptMessage(string messageHash);
        Task<IEnumerable<Message>> ReadAllByChatIncludingAuthorAsync(int chatId);
        
    }
}
