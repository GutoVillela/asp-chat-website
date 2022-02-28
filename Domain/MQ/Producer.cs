using DomainCore.MQ;
using RabbitMQ.Client;
using Shared.MQ;
using System.Text;
using System.Text.Json;
using Shared.Extensions;
using DomainCore.ValueObjects;

namespace Domain.MQ
{
    public class Producer : IProducer
    {
        private readonly IConnectionFactory _factory;

        public Producer(IConnectionFactory factory)
        {
            _factory = factory;
        }

        public void PublishMessage(IMessageMQ message)
        {
            if (!message.IsValid)
                throw new ArgumentException(message.GetNotificationsError());

            using (var connection = _factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare(
                        queue: QueueNameManager.GetQueueName(message.ChatId),
                        durable: true,
                        exclusive: false,
                        autoDelete: false,
                        arguments: null
                        );

                    var stringMessage = JsonSerializer.Serialize(message.MessageId);
                    var byteArray = Encoding.UTF8.GetBytes(stringMessage);

                    channel.BasicPublish(
                        exchange: "",
                        routingKey: QueueNameManager.GetQueueName(message.ChatId),
                        basicProperties: null,
                        body: byteArray);
                }
            }
        }
    }
}
