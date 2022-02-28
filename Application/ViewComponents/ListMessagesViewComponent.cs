using Application.Models;
using Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Application.ViewComponents
{
    public class ListMessagesViewComponent : ViewComponent
    {
        private readonly IMessageApplicationService _messageService;

        public ListMessagesViewComponent(IMessageApplicationService messageService)
        {
            _messageService = messageService;
        }

        public async Task<IViewComponentResult> InvokeAsync(int selectedChatId)
        {
            IEnumerable<MessageViewModel> messages = new List<MessageViewModel>();

            messages = await _messageService.GetAllMessagesByChatRoom(chatRoomId: selectedChatId);

            return View(messages);
        }
    }
}
