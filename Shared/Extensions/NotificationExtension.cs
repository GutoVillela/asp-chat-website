using Flunt.Notifications;
using Shared.Validations;

namespace Shared.Extensions
{
    public static class NotificationExtension
    {
        public static string GetNotificationsError<T>(this T obj) where T : IValidatable<Notification>
        {
            return obj.Notifications?.Select(x => x.ToString() + ". ")?.ToString() ?? string.Empty;
        }
    }
}
