using DomainCore.ValueObjects;

namespace DomainCore.MQ
{
    public interface IConsumer
    {
        void ConsumeMessage(IChannelMQ channelToConsume, Action<IMessageMQ> callBack);
    }
}
