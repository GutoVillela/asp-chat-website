using Shared.ValueObjects;

namespace DomainCore.Commands
{
    public interface ICommandResult
    {
        bool Success { get; }
        string Message { get; }
        Error Error { get; }
    }
}
