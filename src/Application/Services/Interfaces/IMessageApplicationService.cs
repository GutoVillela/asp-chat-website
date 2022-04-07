using Application.Models;
using DomainCore.Commands;

namespace Application.Services.Interfaces
{
    public interface IMessageApplicationService
    {
        Task<ICommandResult> SendMessageAsync(int chatRoomId, string message, string userId);
        Task<IEnumerable<MessageViewModel>> GetAllMessagesByChatRoom(int chatRoomId, int messagesToGet);
    }
}
