using DomainCore.Commands;

namespace DomainCore.Handlers
{
    public interface ICommandHandler<T> where T : ICommand
    {
        Task<ICommandResult> HandleAsync(T command);
    }
}
