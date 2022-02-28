using Flunt.Notifications;
using Flunt.Validations;
using Shared.Commands;
using Shared.Constants;
using Shared.Validations;

namespace Domain.Commands.SendMessage
{
    public class SendMessageCommand : Notifiable<Notification>, IValidatable<Notification>, ICommand
    {
        public string? AuthorId { get; set; }
        public int? ChatRoomId { get; set; }
        public string? Message { get; set; }

        public void Validate()
        {
            AddNotifications(new Contract<Notification>()
                .Requires()
                .IsNotNullOrEmpty(AuthorId, nameof(AuthorId), CommandValidations.ERROR_EMPTY_MESSAGE_AUTHOR)
                .IsNotNull(ChatRoomId, nameof(ChatRoomId), CommandValidations.ERROR_NULL_CHATROOM_ID)
                .IsNotNullOrEmpty(Message, nameof(Message), CommandValidations.ERROR_EMPTY_MESSAGE_TEXT)
            );
        }
    }
}
