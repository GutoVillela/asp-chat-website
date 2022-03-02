using Flunt.Notifications;
using Flunt.Validations;
using DomainCore.Commands;
using Shared.Constants;
using Shared.Validations;

namespace Domain.Commands.CreateChatRoom
{
    public class CreateChatRoomCommand : Notifiable<Notification>, IValidatable<Notification>, ICommand
    {
        public string? Name { get; set; }

        public string? UserId { get; set; }

        public void Validate()
        {
            AddNotifications(new Contract<Notification>()
                .Requires()
                .IsNotNullOrEmpty(Name, nameof(Name), CommandValidations.ERROR_EMPTY_CHATROOM_NAME)
                .IsNotNullOrEmpty(UserId, nameof(UserId), CommandValidations.ERROR_EMPTY_CHATROOM_USER)
            );
        }
    }
}
