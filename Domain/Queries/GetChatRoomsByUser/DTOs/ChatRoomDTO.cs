namespace Domain.Queries.GetChatRoomsByUser.DTOs
{
    public class ChatRoomDTO
    {
        public int ChatRoomId { get; set; }
        public string Name { get; set; }
        public IEnumerable<string> UsersIds { get; set; }
    }
}
