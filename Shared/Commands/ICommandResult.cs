using Shared.ValueObjects;

namespace Shared.Commands
{
    public interface ICommandResult
    {
        bool Success { get; }
        string Message { get; }
        Error Error { get; }
    }
}
