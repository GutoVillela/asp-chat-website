using DomainCore.ValueObjects;

namespace DomainCore.MQ
{
    public interface IProducer
    {
        void PublishMessage(IMessageMQ message);
    }
}
