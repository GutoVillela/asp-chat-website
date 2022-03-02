using Flunt.Notifications;
using Shared.Validations;

namespace Shared.ValueObjects
{
    public abstract class ValueObject :  Notifiable<Notification>, IValidatable<Notification>
    {

    }
}
