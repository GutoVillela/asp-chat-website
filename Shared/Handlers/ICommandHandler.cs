using Shared.Commands;

namespace Shared.Handlers
{
    public interface ICommandHandler<T> where T : ICommand
    {
        Task<ICommandResult> HandleAsync(T command);
    }
}
