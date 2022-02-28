using Domain.Queries.GetChatRoomsByUser.DTOs;
using Shared.Queries;

namespace Domain.Queries.GetChatRoomsByUser
{
    public class GetChatRoomsByUserQueryResult : IQueryResult
    {
        public IEnumerable<ChatRoomDTO> ChatRooms { get; set; }
    }
}
