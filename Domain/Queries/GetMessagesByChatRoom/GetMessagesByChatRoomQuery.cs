using Shared.Queries;

namespace Domain.Queries.GetMessagesByChatRoom
{
    public class GetMessagesByChatRoomQuery : IQueryRequest
    {
        public int ChatRoomId { get; set; }
    }
}
