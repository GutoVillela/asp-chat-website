using DomainCore.ValueObjects;

namespace DomainCore.Bot
{
    public interface IBot
    {
        Task ProcessCommand(IChatCommand chatCommand);
    }
}
