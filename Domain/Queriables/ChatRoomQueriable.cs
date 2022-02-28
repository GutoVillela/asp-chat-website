using Domain.Entities;
using Shared.Queriables;
using System.Linq.Expressions;

namespace Domain.Queriables
{
    public class ChatRoomQueriable : QueriableBase<ChatRoom>
    {
        public static Expression<Func<ChatRoom, IReadOnlyCollection<User>?>> IncludeUsers()
        {
            return x => x.Users;
        }

        public static Expression<Func<ChatRoom, bool>> GetByUserId(string userId)
        {
            return x => x.Users.Any(user => user.Id == userId);
        }
    }
}
