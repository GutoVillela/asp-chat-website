using Domain.Entities;
using Domain.Queries.GetChatRoomsByUser.DTOs;
using Domain.Repositories;
using DomainCore.Handlers;

namespace Domain.Queries.GetChatRoomsByUser
{
    public class GetChatRoomsByUserQueryHandler : IQueryHandler<GetChatRoomsByUserQuery, GetChatRoomsByUserQueryResult>
    {
        private readonly IChatRoomRepository _chatRoomRepository;

        public GetChatRoomsByUserQueryHandler(IChatRoomRepository chatRoomRepository)
        {
            _chatRoomRepository = chatRoomRepository;
        }

        public async Task<GetChatRoomsByUserQueryResult> HandleAsync(GetChatRoomsByUserQuery command)
        {
            var chatRooms = await _chatRoomRepository.ReadAllByUserIncludingUsersAsync(command.UserId);

            IList<ChatRoomDTO> chatRoomDTOs = new List<ChatRoomDTO>();

            foreach (var chatRoom in chatRooms)
                chatRoomDTOs.Add(ConvertEntityToDTO(chatRoom));

            GetChatRoomsByUserQueryResult result = new();
            result.ChatRooms = chatRoomDTOs;
            return result;

        }

        private ChatRoomDTO ConvertEntityToDTO(ChatRoom chatRoom)
        {
            return new ChatRoomDTO
            {
                ChatRoomId = chatRoom.Id,
                Name = chatRoom.Name,
                UsersIds = chatRoom.Users.Select(x => x.Id).ToArray()
            };
        }
    }
}
