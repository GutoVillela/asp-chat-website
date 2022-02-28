using Domain.Entities;
using Shared.Repositories;

namespace Domain.Repositories
{
    public interface IChatRoomRepository : IRepository<ChatRoom>
    {
        Task<IEnumerable<ChatRoom>> ReadAllByUserIncludingUsersAsync(string userId);
    }
}
