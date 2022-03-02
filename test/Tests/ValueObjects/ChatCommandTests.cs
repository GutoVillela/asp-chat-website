using Domain.ValueObjects;
using Xunit;

namespace Tests.ValueObjects
{
    public class ChatCommandTests
    {
        private readonly ChatCommand _chatCommand;

        public ChatCommandTests()
        {
            _chatCommand = new();
            _chatCommand.ChatId = 0;
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("abc")]
        [InlineData("/stock=")]
        [InlineData("/stock= ")]
        public void ShouldReturnFalseWhenTheChatCommandIsNotValid(string? command)
        {
            _chatCommand.Command = command;
            _chatCommand.Validate();
            Assert.False(_chatCommand.IsValid);
        }

        [Theory]
        [InlineData("/stock=AAPL.US")]
        [InlineData("/stock=Bitcoin")]
        [InlineData("/stock=xauusd")]
        [InlineData("/stock=gbppln")]
        [InlineData("/stock=msf.de")]
        public void ShouldReturnTrueWhenTheChatCommandIsValid(string? command)
        {
            _chatCommand.Command = command;
            _chatCommand.Validate();
            Assert.True(_chatCommand.IsValid);
        }

        [Theory]
        [InlineData("AAPL.US")]
        [InlineData("Bitcoin")]
        [InlineData("xauusd")]
        [InlineData("gbppln")]
        [InlineData("msf.de")]
        public void ShouldReturnTheCorrectCommandStockCode(string stockCode)
        {
            _chatCommand.Command = $"/stock={stockCode}";
            Assert.Equal(expected: stockCode, actual: _chatCommand.StockCode);
        }
    }
}
