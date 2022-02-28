using Domain.Entities;
using Domain.Queries.GetMessagesByChatRoom.DTOs;
using Domain.Repositories;
using Shared.Handlers;

namespace Domain.Queries.GetMessagesByChatRoom
{
    public class GetMessagesByChatRoomQueryHandler : IQueryHandler<GetMessagesByChatRoomQuery, GetMessagesByChatRoomQueryResult>
    {

        private readonly IMessageRepository _messageRepository;

        public GetMessagesByChatRoomQueryHandler(IMessageRepository messageRepository)
        {
            _messageRepository = messageRepository;
        }

        public async Task<GetMessagesByChatRoomQueryResult> HandleAsync(GetMessagesByChatRoomQuery command)
        {
            var messages = await _messageRepository.ReadAllByChatIncludingAuthorAsync(command.ChatRoomId);

            IList<MessageDTO> messagesDTOs = new List<MessageDTO>();

            foreach (var message in messages)
                messagesDTOs.Add(ConvertEntityToDTO(message));

            GetMessagesByChatRoomQueryResult result = new();
            result.Messages = messagesDTOs;
            return result;

        }

        private MessageDTO ConvertEntityToDTO(Message messageEntity)
        {
            return new MessageDTO
            {
                MessageText = _messageRepository.DecryptMessage(messageEntity.MessageHash),
                AuthorId = messageEntity.AuthorId,
                AuthorName = messageEntity.Author?.UserName ?? string.Empty,
                SentAt = messageEntity.CreationDate
            };
        }
    }
}
