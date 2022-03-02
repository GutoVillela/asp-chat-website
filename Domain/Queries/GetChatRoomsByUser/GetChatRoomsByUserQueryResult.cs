using Domain.Queries.GetChatRoomsByUser.DTOs;
using DomainCore.Queries;

namespace Domain.Queries.GetChatRoomsByUser
{
    public class GetChatRoomsByUserQueryResult : IQueryResult
    {
        public IEnumerable<ChatRoomDTO> ChatRooms { get; set; }
    }
}
