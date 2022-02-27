using DomainCore.ValueObjects;
using Flunt.Notifications;
using Flunt.Validations;
using Shared.Constants;
using Shared.ValueObjects;

namespace Domain.ValueObjects
{
    public class MessageMQ : ValueObject, IMessageMQ
    {
        public MessageMQ(int messageId, int chatId)
        {
            MessageId = messageId;
            ChatId = chatId;
            ValidateMessageMQ();
        }

        public int MessageId { get; set; }
        public int ChatId { get; set; }

        private void ValidateMessageMQ()
        {
            AddNotifications(new Contract<Notification>()
                .Requires()
                .IsNotNull(MessageId, nameof(MessageId), ValueObjectValidations.ERROR_NULL_MESSAGE_ID)
                .IsNotNull(ChatId, nameof(ChatId), ValueObjectValidations.ERROR_NULL_CHAT_ID)
            );
        }
    }
}
