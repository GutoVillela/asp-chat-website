using Domain.Entities;
using Shared.Constants;
using Shared.Extensions;
using System.Collections.Generic;
using Xunit;

namespace Tests.Extensions
{
    public class NotificationExtensionTests
    {
        [Fact]
        public void ShouldReturnTheCorrectSingleLineOfValidationError()
        {
            Message invalidMessage = new(
                author: null,
                messageHash: "Imagine a hashed message here",
                chatRoom: new ChatRoom(name: "Other chatrooms", new List<User>())
                );

            string errorMessage = invalidMessage.GetNotificationsError();
            Assert.Contains(EntityValidations.ERROR_NULL_MESSAGE_AUTHOR, errorMessage);
        }

        [Fact]
        public void ShouldReturnTheCorrectMultipleLineOfValidationError()
        {
            Message invalidMessage = new(
                author: null,
                messageHash: "Imagine a hashed message here",
                chatRoom: null
                );

            string errorMessage = invalidMessage.GetNotificationsError();
            Assert.True(errorMessage.Contains(EntityValidations.ERROR_NULL_MESSAGE_AUTHOR) && errorMessage.Contains(EntityValidations.ERROR_NULL_MESSAGE_CHATROOM));
        }
    }
}
