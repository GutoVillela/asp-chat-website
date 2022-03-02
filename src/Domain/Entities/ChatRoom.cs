using Flunt.Notifications;
using Flunt.Validations;
using Shared.Constants;
using DomainCore.Entities;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class ChatRoom : Entity
    {
        /// <summary>
        /// Suitable construtor to help EF Map this Entity. 
        /// To use this within the Domain layer please prefer the constructor ChatRoom(string name, IReadOnlyCollection<User> users).
        /// </summary>
        protected ChatRoom(string name)
        {
            Name = name;
            ValidateChatRoom();
        }

        public ChatRoom(string name, IReadOnlyCollection<User> users)
        {
            Name = name;
            Users = users;
            ValidateChatRoom();
        }

        [Required]
        public string Name { get; private set; }

        [Required]
        [MinLength(1)]
        public IReadOnlyCollection<User> Users { get; private set; }

        public IReadOnlyCollection<Message>? Messages { get; private set; }

        private void ValidateChatRoom()
        {
            AddNotifications(new Contract<Notification>()
                .Requires()
                .IsNotNullOrEmpty(Name, nameof(Name), EntityValidations.ERROR_EMPTY_CHATROOM_NAME)
                .IsNotNull(Users, nameof(Users), EntityValidations.ERROR_NULL_CHATROOM_USERS)
                .IsTrue(Users is not null && Users.Any(), nameof(Users), EntityValidations.ERROR_EMPTY_CHATROOM_USERS)
            );
        }

    }
}
