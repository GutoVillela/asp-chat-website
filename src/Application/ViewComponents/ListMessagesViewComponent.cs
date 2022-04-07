using Application.Configuration;
using Application.Models;
using Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Application.ViewComponents
{
    public class ListMessagesViewComponent : ViewComponent
    {
        private readonly IMessageApplicationService _messageService;
        private readonly ChatConfiguration _chatConfiguration;

        public ListMessagesViewComponent(IMessageApplicationService messageService, ChatConfiguration chatConfiguration)
        {
            _messageService = messageService;
            _chatConfiguration = chatConfiguration;
        }

        public async Task<IViewComponentResult> InvokeAsync(int selectedChatId)
        {
            IEnumerable<MessageViewModel> messages = new List<MessageViewModel>();

            messages = await _messageService.GetAllMessagesByChatRoom(chatRoomId: selectedChatId, _chatConfiguration.MaxMessagesPerChat);

            return View(messages);
        }
    }
}
