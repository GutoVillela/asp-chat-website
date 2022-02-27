using Application.Models;
using Application.Services.Interfaces;
using Domain.Commands.CreateChatRoom;
using Shared.Commands;
using Shared.Handlers;

namespace Application.Services
{
    public class ChatRoomApplicationService : IChatRoomApplicationService
    {
        private readonly ICommandHandler<CreateChatRoomCommand> _createChatRoomHandler;

        public ChatRoomApplicationService(ICommandHandler<CreateChatRoomCommand> createChatRoomHandler)
        {
            _createChatRoomHandler = createChatRoomHandler;
        }

        public async Task<ICommandResult> CreateChatRoomAsync(ChatRoomViewModel chatRoom)
        {
            CreateChatRoomCommand command = new();
            command.Name = chatRoom.Name;
            command.UserId = chatRoom.UserId;
            return await _createChatRoomHandler.HandleAsync(command);
        }
    }
}
