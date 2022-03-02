namespace Application.Configuration
{
    public class MQConfiguration
    {
        public const string ConfigurationName = "RabbitMQ";
        private const string DEFAULT_HOSTNAME = "localhost";
        private const int DEFAULT_PORT = -1;

        public string HostName { get; set; } = DEFAULT_HOSTNAME;
        public int Port { get; set; } = DEFAULT_PORT;
    }
}
