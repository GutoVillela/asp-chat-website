using Domain.Entities;
using DomainCore.Queriables;
using System.Linq.Expressions;

namespace Domain.Queriables
{
    public class MessageQueriable : QueriableBase<Message>
    {
        public static Expression<Func<Message, User?>> IncludeAuthor()
        {
            return x => x.Author;
        }

        public static Expression<Func<Message, bool>> GetByChatRoomId(int chatRoomId)
        {
            return x => x.ChatRoomId == chatRoomId;
        }

        public static Expression<Func<Message, DateTime>> CreationDate()
        {
            return x => x.CreationDate;
        }
    }
}
