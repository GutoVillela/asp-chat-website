using Flunt.Notifications;
using Flunt.Validations;
using Shared.Constants;
using Shared.Entities;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class Message : Entity
    {
        /// <summary>
        /// Suitable construtor to help EF Map this Entity. 
        /// To use this within the Domain layer please prefer the constructor Message(User author, string messageHash, byte[] messageSalt, int messageSaltIterations, ChatRoom chatRoom).
        /// </summary>
        protected Message(string authorId, string messageHash, int chatRoomId)
        {
            AuthorId = authorId;
            MessageHash = messageHash;
            ChatRoomId = chatRoomId;
            ValidateMessage();
        }

        public Message(User author, string messageHash, ChatRoom chatRoom)
        {
            Author = author;
            MessageHash = messageHash;
            ChatRoom = chatRoom;
            ValidateMessage();
        }

        [Required]
        public User Author { get; private set; }

        public string AuthorId { get; private set; }

        [Required]
        public string MessageHash { get; private set; }

        [Required]
        public ChatRoom? ChatRoom { get; private set; }

        public int ChatRoomId { get; private set; }

        private void ValidateMessage()
        {
            AddNotifications(new Contract<Notification>()
                .Requires()
                .IsNotNull(Author, nameof(Author), EntityValidations.ERROR_NULL_MESSAGE_AUTHOR)
                .IsNotNullOrEmpty(MessageHash, nameof(MessageHash), EntityValidations.ERROR_EMPTY_MESSAGE_HASH)
                .IsNotNull(ChatRoom, nameof(ChatRoom), EntityValidations.ERROR_NULL_MESSAGE_CHATROOM)
            );
        }
    }
}