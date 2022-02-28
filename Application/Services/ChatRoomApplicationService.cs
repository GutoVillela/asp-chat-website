using Application.Models;
using Application.Services.Interfaces;
using Domain.Commands.CreateChatRoom;
using Domain.Queries.GetChatRoomsByUser;
using Domain.Queries.GetChatRoomsByUser.DTOs;
using Shared.Commands;
using Shared.Handlers;

namespace Application.Services
{
    public class ChatRoomApplicationService : IChatRoomApplicationService
    {
        private readonly ICommandHandler<CreateChatRoomCommand> _createChatRoomHandler;
        private readonly IQueryHandler<GetChatRoomsByUserQuery, GetChatRoomsByUserQueryResult> _getChatRoomsByUserHandler;

        public ChatRoomApplicationService(
            ICommandHandler<CreateChatRoomCommand> createChatRoomHandler, 
            IQueryHandler<GetChatRoomsByUserQuery, GetChatRoomsByUserQueryResult> getChatRoomsByUserHandler)
        {
            _createChatRoomHandler = createChatRoomHandler;
            _getChatRoomsByUserHandler = getChatRoomsByUserHandler;
        }

        public async Task<ICommandResult> CreateChatRoomAsync(ChatRoomViewModel chatRoom)
        {
            CreateChatRoomCommand command = new();
            command.Name = chatRoom.Name;
            command.UserId = chatRoom.UserId;
            return await _createChatRoomHandler.HandleAsync(command);
        }

        public async Task<IEnumerable<ChatRoomViewModel>> GetAllByUserId(string userId)
        {
            GetChatRoomsByUserQuery query = new();
            query.UserId = userId;
            var chatRooms = await _getChatRoomsByUserHandler.HandleAsync(query);

            IList<ChatRoomViewModel> chatRoomViewModels = new List<ChatRoomViewModel>();
            foreach (var chatRoom in chatRooms.ChatRooms)
                chatRoomViewModels.Add(ConvertDTOToViewModel(chatRoom));

            return chatRoomViewModels;
            
        }

        private ChatRoomViewModel ConvertDTOToViewModel(ChatRoomDTO chatRoomDTO)
        {
            return new ChatRoomViewModel
            {
                Id = chatRoomDTO.ChatRoomId,
                Name = chatRoomDTO.Name
            };
        }
    }
}
