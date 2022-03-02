using Flunt.Notifications;
using Shared.Validations;
using Shared.ValueObjects;

namespace DomainCore.ValueObjects
{
    public interface IMessageMQ : IValidatable<Notification>
    {
        public string MessageHash { get; set; }
        public string AuthorName { get; set; }
        public int ChatId { get; set; }
        public bool IsErrorMessage { get; set; }
    }
}
