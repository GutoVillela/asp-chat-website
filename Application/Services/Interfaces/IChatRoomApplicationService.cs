using Application.Models;
using Shared.Commands;

namespace Application.Services.Interfaces
{
    public interface IChatRoomApplicationService
    {
        Task<ICommandResult> CreateChatRoomAsync(ChatRoomViewModel chatRoom);
        Task<IEnumerable<ChatRoomViewModel>> GetAll();
        Task<IEnumerable<ChatRoomViewModel>> GetAllByUserId(string userId);

    }
}
