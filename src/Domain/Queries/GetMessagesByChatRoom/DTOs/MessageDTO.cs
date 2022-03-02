namespace Domain.Queries.GetMessagesByChatRoom.DTOs
{
    public class MessageDTO
    {
        public string MessageText { get; set; }
        public DateTime SentAt { get; set; }
        public string AuthorId { get; set; }
        public string AuthorName { get; set; }
    }
}
