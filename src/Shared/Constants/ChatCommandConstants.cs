namespace Shared.Constants
{
    public static class ChatCommandConstants
    {
        public const string REGEX_COMMAND_PATTERN = @"/stock=(?<" + REGEX_COMMAND_STOCK_CODE_GROUP + @">\w+\.?\w*)";
        public const string REGEX_COMMAND_STOCK_CODE_GROUP = "stock_code";
    }
}
