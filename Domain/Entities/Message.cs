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
        protected Message(string authorId, string messageHash, byte[] messageSalt, int messageSaltIterations, int chatRoomId)
        {
            AuthorId = authorId;
            MessageHash = messageHash;
            MessageSalt = messageSalt;
            MessageSaltIterations = messageSaltIterations;
            ChatRoomId = chatRoomId;
            ValidateMessage();
        }

        public Message(User author, string messageHash, byte[] messageSalt, int messageSaltIterations, ChatRoom chatRoom)
        {
            Author = author;
            MessageHash = messageHash;
            MessageSalt = messageSalt;
            MessageSaltIterations = messageSaltIterations;
            ChatRoom = chatRoom;
            ValidateMessage();
        }

        [Required]
        public User Author { get; private set; }

        public string AuthorId { get; private set; }

        [Required]
        public string MessageHash { get; private set; }

        [Required]
        public byte[] MessageSalt { get; private set; }

        [Required]
        public int MessageSaltIterations { get; private set; }

        [Required]
        public ChatRoom? ChatRoom { get; private set; }

        public int ChatRoomId { get; private set; }

        private void ValidateMessage()
        {
            AddNotifications(new Contract<Notification>()
                .Requires()
                .IsNotNull(Author, nameof(Author), EntityValidations.ERROR_NULL_MESSAGE_AUTHOR)
                .IsNotNullOrEmpty(MessageHash, nameof(MessageHash), EntityValidations.ERROR_EMPTY_MESSAGE_HASH)
                .IsNotNull(MessageSalt, nameof(MessageSalt), EntityValidations.ERROR_NULL_MESSAGE_SALT)
                .IsGreaterThan(MessageSaltIterations, 0, nameof(MessageSaltIterations), EntityValidations.ERROR_LOW_MESSAGE_SALT_ITERATIONS)
                .IsNotNull(ChatRoom, nameof(ChatRoom), EntityValidations.ERROR_NULL_MESSAGE_CHATROOM)
            );
        }
    }
}
