using Application.Models;
using Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Application.Controllers
{
    public class ChatRoomsController : Controller
    {
        private readonly IChatRoomApplicationService _service;

        public ChatRoomsController(IChatRoomApplicationService service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateAsync(ChatRoomViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _service.CreateChatRoomAsync(model);

                if (result.Success)
                {
                    return RedirectToAction(nameof(Index), "Home");
                }

                AddErrorsToModelState(errorMessage: result.Error.ToString());
                return View(model);
            }
            else
            {
                return View(model);
            }
        }

        protected void AddErrorsToModelState(string errorMessage)
        {
            ModelState.AddModelError(nameof(ChatRoomViewModel.Name), errorMessage);
        }
    }
}
