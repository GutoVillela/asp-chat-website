using Application.Models;
using Shared.Commands;

namespace Application.Services.Interfaces
{
    public interface IMessageApplicationService
    {
        Task<ICommandResult> SendMessageAsync(int chatRoomId, string message, string userId);
        Task<IEnumerable<MessageViewModel>> GetAllMessagesByChatRoom(int chatRoomId);
    }
}
