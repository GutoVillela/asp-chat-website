using DomainCore.ValueObjects;
using Flunt.Notifications;
using Flunt.Validations;
using Shared.Constants;
using Shared.ValueObjects;

namespace Domain.ValueObjects
{
    public class ChannelMQ : ValueObject, IChannelMQ
    {
        public ChannelMQ(int chatId)
        {
            ChatId = chatId;
            ValidateMessageMQ();
        }

        public int ChatId { get; set; }

        private void ValidateMessageMQ()
        {
            AddNotifications(new Contract<Notification>()
                .Requires()
                .IsNotNull(ChatId, nameof(ChatId), ValueObjectValidations.ERROR_NULL_CHAT_ID)
            );
        }
    }
}
