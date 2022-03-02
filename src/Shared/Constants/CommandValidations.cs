namespace Shared.Constants
{
    public static class CommandValidations
    {
        #region ChatRoom Command
        public const string ERROR_EMPTY_CHATROOM_NAME = "The chat room name can't be empty";
        public const string ERROR_EMPTY_CHATROOM_USER = "The user who created the chatroom must be associated to the chat room";
        public const string SUCCESS_ON_CREATE_CHATROOM_COMMAND = "The chatroom was created successfully";
        #endregion ChatRoom Command

        #region SendMessage Command
        public const string ERROR_EMPTY_MESSAGE_AUTHOR = "The message author can't be empty";
        public const string ERROR_NULL_CHATROOM_ID = "The chatroom ID can't be null";
        public const string ERROR_EMPTY_MESSAGE_TEXT = "The message can't be empty";
        public const string SUCCESS_ON_SEND_MESSAGE_COMMAND = "The message was sent successfully";
        public const string SUCCESS_ON_SEND_CHAT_COMMAND_TO_BOT = "The chat command was sent to the bot";
        #endregion SendMessage Command
    }
}
