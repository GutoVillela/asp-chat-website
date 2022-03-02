namespace Application.Infrastructure
{
    public static class HubManager
    {
        public const string MESSAGE_HUB_ENDPOINT = "/messageHub";
        public const string MESSAGE_HUB_RECEIVE_MESSAGE_METHOD = "ReceiveMessage";
        public const string MESSAGE_HUB_NOTIFY_ERROR_METHOD = "NotifyError";
    }
}
