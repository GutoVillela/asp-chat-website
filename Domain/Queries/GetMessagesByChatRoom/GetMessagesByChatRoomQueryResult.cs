using Domain.Queries.GetMessagesByChatRoom.DTOs;
using Shared.Queries;

namespace Domain.Queries.GetMessagesByChatRoom
{
    public class GetMessagesByChatRoomQueryResult : IQueryResult
    {
        public IEnumerable<MessageDTO> Messages { get; set; }
    }
}
