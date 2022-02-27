using Flunt.Notifications;
using Shared.Validations;
using Shared.ValueObjects;

namespace DomainCore.ValueObjects
{
    public interface IMessageMQ : IValidatable<Notification>
    {
        public int MessageId { get; set; }
        public int ChatId { get; set; }
    }
}
