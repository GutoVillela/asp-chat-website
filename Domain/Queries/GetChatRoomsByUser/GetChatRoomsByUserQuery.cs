using Shared.Queries;

namespace Domain.Queries.GetChatRoomsByUser
{
    public class GetChatRoomsByUserQuery : IQueryRequest
    {
        public string? UserId { get; set; }
    }
}
