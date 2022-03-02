using Flunt.Notifications;
using Shared.Validations;

namespace DomainCore.ValueObjects
{
    public interface IChatCommand : IValidatable<Notification>
    {
        public string StockCode { get; }
        public int? ChatId { get; set; }
        bool IsChatCommand(string? command);
        void Validate();
    }
}
