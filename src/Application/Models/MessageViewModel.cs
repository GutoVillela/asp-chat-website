namespace Application.Models
{
    public class MessageViewModel
    {
        public int Id { get; set; }
        public int ChatRoomId { get; set; }
        public string Message { get; set; }
        public string AuthorId { get; set; }
        public string AuthorName { get; set; }
        public DateTime SentAt { get; set; }
    }
}
