using DomainCore.ValueObjects;
using Flunt.Notifications;
using Flunt.Validations;
using Shared.Constants;
using Shared.ValueObjects;
using System.Text.RegularExpressions;

namespace Domain.ValueObjects
{
    public class ChatCommand : ValueObject, IChatCommand
    {
        public string? Command { get; set; }
        public int? ChatId { get; set; }

        public string StockCode
        {
            get
            {
                if (!IsChatCommand(Command))
                    return string.Empty;

                Match match = new Regex(ChatCommandConstants.REGEX_COMMAND_PATTERN).Match(Command!);
                return match.Groups[ChatCommandConstants.REGEX_COMMAND_STOCK_CODE_GROUP].Value;
            }
        }


        public void Validate()
        {
            AddNotifications(new Contract<Notification>()
                .Requires()
                .IsNotNullOrEmpty(Command, nameof(Command), ValueObjectValidations.ERROR_EMPTY_CHAT_COMMAND)
                .IsNotNull(ChatId, nameof(ChatId), ValueObjectValidations.ERROR_NULL_CHAT_ID)
                .IsTrue(IsChatCommand(Command), nameof(Command), ValueObjectValidations.ERROR_INVALID_CHAT_COMMAND)
            );
        }

        public bool IsChatCommand(string? command)
        {
            if (string.IsNullOrEmpty(command))
                return false;

            Regex commandPattern = new(ChatCommandConstants.REGEX_COMMAND_PATTERN);
            return commandPattern.IsMatch(command);
        }
    }
}
