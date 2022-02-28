using Flunt.Notifications;
using Microsoft.AspNetCore.Identity;
using Shared.Constants;
using Shared.Entities;
using Shared.Validations;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace Domain.Entities
{
    public class User : IdentityUser, IEntity
    {
        private readonly IList<Notification> _notifications;

        public User()
        {
            _notifications = new List<Notification>();
            ValidateUser();
        }

        public static ClaimsIdentity Identity { get; set; }
        public IReadOnlyCollection<ChatRoom>? ChatRooms { get; private set; }

        public IReadOnlyCollection<Message>? Messages { get; private set; }

        #region Validation Attributes
        public bool IsValid => !_notifications.Any();

        public IReadOnlyCollection<Notification> Notifications => _notifications as IReadOnlyCollection<Notification> ?? new List<Notification>();
        #endregion Validation Attributes

        private void ValidateUser()
        {
            if (string.IsNullOrEmpty(UserName))
                _notifications.Add(new Notification(key: nameof(UserName), EntityValidations.ERROR_EMPTY_USER_NAME));
        }
    }
}
