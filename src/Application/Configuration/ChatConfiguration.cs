namespace Application.Configuration
{
    public class ChatConfiguration
    {
        public const string ConfigurationName = "ChatConfiguration";
        private const int DEFAULT_MAX_MESSAGES_PER_CHAT = 50;

        public int MaxMessagesPerChat { get; set; } = DEFAULT_MAX_MESSAGES_PER_CHAT;
    }
}
