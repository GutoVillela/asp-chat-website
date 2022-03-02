using Domain.ValueObjects;
using DomainCore.MQ;
using DomainCore.ValueObjects;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Shared.Extensions;
using Shared.MQ;
using System.Text;
using System.Text.Json;

namespace Infrastructure.MQ
{
    public class Consumer : IConsumer
    {
        private readonly IConnectionFactory _factory;

        public Consumer(IConnectionFactory factory)
        {
            _factory = factory;
        }

        public void ConsumeMessage(IChannelMQ channelToConsume, Action<IMessageMQ> callBack)
        {
            if (!channelToConsume.IsValid)
                throw new ArgumentException(channelToConsume.GetNotificationsError());

            var connection = _factory.CreateConnection();
            var channel = connection.CreateModel();

            channel.QueueDeclare(
                queue: QueueNameManager.GetQueueName(channelToConsume.ChatId),
                durable: true,
                exclusive: false,
                autoDelete: false,
                arguments: null
                );

            channel.BasicQos(prefetchSize: 0, prefetchCount: 3, global: false);

            var consumer = new EventingBasicConsumer(channel);

            consumer.Received += (model, eventArgs) =>
            {
                var body = eventArgs.Body;
                var message = Encoding.UTF8.GetString(body.ToArray());

                IMessageMQ messageMQ = JsonSerializer.Deserialize<MessageMQ>(message);
                callBack(messageMQ);

                channel.BasicAck(eventArgs.DeliveryTag, false);
            };

            channel.BasicConsume(queue: QueueNameManager.GetQueueName(channelToConsume.ChatId),
                                    autoAck: false,
                                    consumer: consumer);


        }
    }
}
