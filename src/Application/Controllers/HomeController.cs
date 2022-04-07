using Application.Hubs;
using Application.Models;
using Application.Services.Interfaces;
using Domain.ValueObjects;
using DomainCore.Helpers;
using DomainCore.MQ;
using DomainCore.ValueObjects;
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
        private readonly IConsumer _consumer;
        private readonly ICryptographyHelper _cryptographyHelper;
        private readonly MessageHub _messageHub;
        

        public HomeController(
            IChatRoomApplicationService chatRoomService, 
            IMessageApplicationService messageService, 
            IConsumer consumer, 
            ICryptographyHelper cryptographyHelper, 
            MessageHub messageHub)
        {
            _chatRoomService = chatRoomService;
            _messageService = messageService;
            _consumer = consumer;
            _cryptographyHelper = cryptographyHelper;
            _messageHub = messageHub;
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
            _consumer.ConsumeMessage(new ChannelMQ(chatId: id.Value), NotifyClientOfMQMessage);
            return RedirectToAction(nameof(Index), model);
        }

        [HttpPost]
        public async Task<IActionResult> SendMessageAsync(int? chatRoomId, string? message)
        {
            if (chatRoomId is null || message is null)
                return NotFound();

            var result = await _messageService.SendMessageAsync(chatRoomId: chatRoomId.Value, message: message, userId: GetAutheticatedUserId());

            if (result.Success)
                return Ok();

            return BadRequest(result.Message);
            
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        private async Task<IEnumerable<ChatRoomViewModel>> LoadChatRooms()
        {
            var chatRooms = await _chatRoomService.GetAll();
            return chatRooms;
        }

        private string GetAutheticatedUserId()
        {
            var userId = ((ClaimsIdentity)User.Identity).FindFirst(ClaimTypes.NameIdentifier).Value;
            return userId;
        }

        private void NotifyClientOfMQMessage(IMessageMQ message)
        {
            string messageText = _cryptographyHelper.DecryptMessage(message.MessageHash);

            if (message.IsErrorMessage)
                _messageHub.NotifyError(message: messageText).Wait();
            else
                _messageHub.SendMessage(message: messageText, authorName: message.AuthorName, chatRoomId: message.ChatId).Wait();
        }
    }
}