using Shared.Commands;
using Shared.ValueObjects;

namespace Domain.Commands.Base
{
    public class CommandResult : ICommandResult
    {
        public CommandResult(bool success, string message)
        {
            Success = success;
            Message = message;
        }

        public CommandResult(bool success, string message, Error error)
        {
            Success = success;
            Message = message;
            Error = error;
        }

        public bool Success { get; private set; }
        public string Message { get; private set; }
        public Error? Error { get; private set; }
    }
}
