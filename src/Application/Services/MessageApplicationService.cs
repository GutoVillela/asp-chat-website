using Application.Infrastructure;
using Application.Models;
using Application.Services.Interfaces;
using Domain.Commands.SendMessage;
using Domain.Queries.GetMessagesByChatRoom;
using Domain.Queries.GetMessagesByChatRoom.DTOs;
using DomainCore.Commands;
using DomainCore.Handlers;

namespace Application.Services
{
    public class MessageApplicationService : IMessageApplicationService
    {

        private readonly ICommandHandler<SendMessageCommand> _sendMessageHandler;
        private readonly IQueryHandler<GetMessagesByChatRoomQuery, GetMessagesByChatRoomQueryResult> _getMessagesByChatRoomHandler;

        public MessageApplicationService(ICommandHandler<SendMessageCommand> sendMessageHandler, IQueryHandler<GetMessagesByChatRoomQuery, GetMessagesByChatRoomQueryResult> getMessagesByChatRoomHandler)
        {
            _sendMessageHandler = sendMessageHandler;
            _getMessagesByChatRoomHandler = getMessagesByChatRoomHandler;
        }

        public async Task<ICommandResult> SendMessageAsync(int chatRoomId, string message, string userId)
        {
            SendMessageCommand command = new();
            command.ChatRoomId = chatRoomId;
            command.Message = message;
            command.AuthorId = userId;
            return await _sendMessageHandler.HandleAsync(command);
        }

        public async Task<IEnumerable<MessageViewModel>> GetAllMessagesByChatRoom(int chatRoomId, int messagesToGet)
        {
            GetMessagesByChatRoomQuery query = new();
            query.ChatRoomId = chatRoomId;
            query.MessagesToGet = messagesToGet;
            
            var result = await _getMessagesByChatRoomHandler.HandleAsync(query);

            IList<MessageViewModel> messagesViewModels = new List<MessageViewModel>();
            foreach (var message in result.Messages)
                messagesViewModels.Add(ConvertDTOToViewModel(message));

            return messagesViewModels;
        }

        private MessageViewModel ConvertDTOToViewModel(MessageDTO messageDTO)
        {
            return new MessageViewModel
            {
                Message = messageDTO.MessageText,
                AuthorId = messageDTO.AuthorId,
                AuthorName = messageDTO.AuthorName,
                SentAt = messageDTO.SentAt.ToLocalTime()
    };
        }
    }
}
