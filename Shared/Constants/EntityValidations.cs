namespace Shared.Constants
{
    public static class EntityValidations
    {
        #region Message validations
        public const string ERROR_NULL_MESSAGE_AUTHOR = "The message author can't be null";
        public const string ERROR_EMPTY_MESSAGE_HASH = "The message hash can't be empty";
        public const string ERROR_NULL_MESSAGE_SALT = "The message salt can't be null";
        public const string ERROR_LOW_MESSAGE_SALT_ITERATIONS = "The message salt iteration must be greather then zero";
        public const string ERROR_NULL_MESSAGE_CHATROOM = "The message chatroom can't be null";
        #endregion Message validations

        #region ChatRoom validations
        public const string ERROR_EMPTY_CHATROOM_NAME = "The chatroom name can't be empty";
        public const string ERROR_NULL_CHATROOM_USERS = "The chatroom users can't be null";
        public const string ERROR_EMPTY_CHATROOM_USERS = "There must be at least one chatroom user";
        #endregion ChatRoom validations

        #region User validations
        public const string ERROR_EMPTY_USER_NAME = "The user name can't be empty";
        #endregion User validations
    }
}
