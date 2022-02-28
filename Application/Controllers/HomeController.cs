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

        public HomeController(IChatRoomApplicationService chatRoomService)
        {
            _chatRoomService = chatRoomService;
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

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        private async Task<IEnumerable<ChatRoomViewModel>> LoadChatRooms()
        {
            var userId = ((ClaimsIdentity)User.Identity).FindFirst(ClaimTypes.NameIdentifier).Value;
            var chatRooms = await _chatRoomService.GetAllByUserId(userId);
            return chatRooms;
        }
    }
}