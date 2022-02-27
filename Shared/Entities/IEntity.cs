using Flunt.Notifications;
using Shared.Validations;

namespace Shared.Entities
{
    public interface IEntity : IValidatable<Notification>
    {
    }
}
