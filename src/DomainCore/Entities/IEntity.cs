using Flunt.Notifications;
using Shared.Validations;

namespace DomainCore.Entities
{
    public interface IEntity : IValidatable<Notification>
    {
    }
}
