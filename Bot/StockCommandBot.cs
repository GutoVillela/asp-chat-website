using Bot.Constants;
using Bot.StockCommandUtil;
using Domain.ValueObjects;
using DomainCore.Bot;
using DomainCore.Helpers;
using DomainCore.MQ;
using DomainCore.ValueObjects;
using Shared.Extensions;

namespace Bot
{
    public class StockCommandBot : IBot
    {

        private readonly IProducer _mqProducer;
        private readonly ICryptographyHelper _cryptographyHelper;

        public StockCommandBot(IProducer mqProducer, ICryptographyHelper cryptographyHelper)
        {
            _mqProducer = mqProducer;
            _cryptographyHelper = cryptographyHelper;
        }

        public async Task ProcessCommand(IChatCommand chatCommand)
        {
            if(chatCommand is null)
                return;

            // Validate command
            chatCommand.Validate();
            if (!chatCommand.IsValid)
            {
                var errorHash = _cryptographyHelper.EncryptMessage(string.Format(BotErrorMessages.ERROR_INVALID_COMMAND, chatCommand.GetNotificationsError()));
                _mqProducer.PublishMessage(new MessageMQ(
                    messageHash: errorHash, 
                    authorName: BotInfo.BOT_NAME, 
                    chatId: chatCommand.ChatId!.Value,
                    isErrorMessage: true)
                    );
                return;
            }

            (string stockMessage, bool isError) = await  new StooqRequestManager().GetStockMessageAsync(chatCommand.StockCode);
            var messageHash = _cryptographyHelper.EncryptMessage(stockMessage);
            _mqProducer.PublishMessage(new MessageMQ(messageHash: messageHash, authorName: BotInfo.BOT_NAME, chatId: chatCommand.ChatId!.Value, isErrorMessage: isError));
        }
    }
}
