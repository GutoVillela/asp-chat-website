using System.ComponentModel.DataAnnotations;

namespace Application.Models
{
    public class ChatRoomViewModel
    {
        [Required]
        public string Name { get; set; }

        public string UserId { get; set; }
    }
}
