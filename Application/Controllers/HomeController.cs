using Application.Infrastructure;
using Application.Models;
using Application.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;

namespace Application.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly IChatRoomApplicationService _chatRoomService;
        private readonly IMessageApplicationService _messageService;

        public HomeController(IChatRoomApplicationService chatRoomService, IMessageApplicationService messageService)
        {
            _chatRoomService = chatRoomService;
            _messageService = messageService;
        }

        [HttpGet]
        public async Task<IActionResult> IndexAsync(HomeViewModel model)
        {
            model.Chats = await LoadChatRooms();
            return View(model);
        }

        [HttpGet]
        public IActionResult ViewChat(int? id)
        {
            if (id is null)
                return NotFound();

            HomeViewModel model = new();
            model.SelectedChat = id;
            return RedirectToAction(nameof(Index), model);
        }

        [HttpPost]
        public async Task<IActionResult> SendMessageAsync(int? chatRoomId, string? message)
        {
            if (chatRoomId is null || message is null)
                return NotFound();

            var result = await _messageService.SendMessageAsync(chatRoomId: chatRoomId.Value, message: message, userId: GetAutheticatedUserId());

            if (result.Success)
                return RedirectToAction(nameof(Index), new HomeViewModel { SelectedChat = chatRoomId });

            return BadRequest();
            
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        private async Task<IEnumerable<ChatRoomViewModel>> LoadChatRooms()
        {
            var userId = GetAutheticatedUserId();
            //var chatRooms = await _chatRoomService.GetAllByUserId(userId);
            var chatRooms = await _chatRoomService.GetAll();
            return chatRooms;
        }

        private string GetAutheticatedUserId()
        {
            var userId = ((ClaimsIdentity)User.Identity).FindFirst(ClaimTypes.NameIdentifier).Value;
            return userId;
        }
    }
}