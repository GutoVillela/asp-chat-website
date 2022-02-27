namespace Shared.MQ
{
    public class QueueNameManager
    {
        private const string MESSAGES_QUEUE_PREFIX = "mq_messages_chat_{0}";

        public static string GetQueueName(int chatId) => string.Format(MESSAGES_QUEUE_PREFIX, chatId);
        
    }
}
