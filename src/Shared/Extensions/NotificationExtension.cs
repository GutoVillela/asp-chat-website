using Flunt.Notifications;
using Shared.Validations;

namespace Shared.Extensions
{
    public static class NotificationExtension
    {
        public static string GetNotificationsError<T>(this T obj) where T : IValidatable<Notification>
        {
            if(!obj.Notifications.Any())
                return string.Empty;

            return string.Join(". ", obj.Notifications.Select(x => x.Message + ". "));
        }
    }
}
