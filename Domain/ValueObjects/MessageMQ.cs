using DomainCore.ValueObjects;
using Flunt.Notifications;
using Flunt.Validations;
using Shared.Constants;
using Shared.ValueObjects;

namespace Domain.ValueObjects
{
    public class MessageMQ : ValueObject, IMessageMQ
    {

        public MessageMQ(string messageHash, string authorName, int chatId, bool isErrorMessage)
        {
            MessageHash = messageHash;
            AuthorName = authorName;
            ChatId = chatId;
            IsErrorMessage = isErrorMessage;
            ValidateMessageMQ();
        }
        public int ChatId { get; set; }
        public string AuthorName { get ; set ; }
        public string MessageHash { get; set; }
        public bool IsErrorMessage { get; set; } = false;

        private void ValidateMessageMQ()
        {
            AddNotifications(new Contract<Notification>()
                .Requires()
                .IsNotNullOrEmpty(MessageHash, nameof(MessageHash), ValueObjectValidations.ERROR_EMPTY_MESSAGE_HASH)
                .IsNotNullOrEmpty(AuthorName, nameof(AuthorName), ValueObjectValidations.ERROR_EMPTY_AUTHOR_NAME)
                .IsNotNull(ChatId, nameof(ChatId), ValueObjectValidations.ERROR_NULL_CHAT_ID)
            );
        }
    }
}
