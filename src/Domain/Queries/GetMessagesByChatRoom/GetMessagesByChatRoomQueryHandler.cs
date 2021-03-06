using Domain.Entities;
using Domain.Queries.GetMessagesByChatRoom.DTOs;
using Domain.Repositories;
using DomainCore.Helpers;
using DomainCore.Handlers;

namespace Domain.Queries.GetMessagesByChatRoom
{
    public class GetMessagesByChatRoomQueryHandler : IQueryHandler<GetMessagesByChatRoomQuery, GetMessagesByChatRoomQueryResult>
    {

        private readonly IMessageRepository _messageRepository;
        private readonly ICryptographyHelper _cryptographyHelper;

        public GetMessagesByChatRoomQueryHandler(IMessageRepository messageRepository, ICryptographyHelper cryptographyHelper)
        {
            _messageRepository = messageRepository;
            _cryptographyHelper = cryptographyHelper;
        }

        public async Task<GetMessagesByChatRoomQueryResult> HandleAsync(GetMessagesByChatRoomQuery query)
        {
            var messages = await _messageRepository.ReadAllByChatIncludingAuthorAsync(query.ChatRoomId, query.MessagesToGet);

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
                MessageText = _cryptographyHelper.DecryptMessage(messageEntity.MessageHash),
                AuthorId = messageEntity.AuthorId,
                AuthorName = messageEntity.Author?.UserName ?? string.Empty,
                SentAt = messageEntity.CreationDate
            };
        }
    }
}
