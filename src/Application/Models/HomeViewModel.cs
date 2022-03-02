namespace Application.Models
{
    public class HomeViewModel
    {
        public IEnumerable<ChatRoomViewModel> Chats { get; set; }

        public int? SelectedChat { get; set; }
    }
}
