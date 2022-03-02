using Flunt.Notifications;
using Shared.Validations;

namespace DomainCore.ValueObjects
{
    public interface IChannelMQ : IValidatable<Notification>
    {
        public int ChatId { get; set; }
    }
}
