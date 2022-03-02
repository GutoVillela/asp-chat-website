using Domain.Commands.Base;
using Domain.Entities;
using Domain.Repositories;
using Domain.ValueObjects;
using DomainCore.Bot;
using DomainCore.Helpers;
using DomainCore.MQ;
using DomainCore.ValueObjects;
using Flunt.Notifications;
using DomainCore.Commands;
using Shared.Constants;
using Shared.Extensions;
using DomainCore.Handlers;
using Shared.Repositories;
using Shared.Validations;
using Shared.ValueObjects;

namespace Domain.Commands.SendMessage
{
    public class SendMessageCommandHandler : Notifiable<Notification>, IValidatable<Notification>, ICommandHandler<SendMessageCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IChatRoomRepository _chatRoomRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMessageRepository _messageRepository;
        private readonly IProducer _mqProducer;
        private readonly ICryptographyHelper _cryptographyHelper;
        private readonly IBot _bot;

        public SendMessageCommandHandler(
            IUnitOfWork unitOfWork,
            IChatRoomRepository chatRoomRepository,
            IUserRepository userRepository,
            IMessageRepository messageRepository,
            IProducer mqProducer,
            ICryptographyHelper cryptographyHelper,
            IBot bot)
        {
            _unitOfWork = unitOfWork;
            _chatRoomRepository = chatRoomRepository;
            _userRepository = userRepository;
            _messageRepository = messageRepository;
            _mqProducer = mqProducer;
            _cryptographyHelper = cryptographyHelper;
            _bot = bot;
        }

        public async Task<ICommandResult> HandleAsync(SendMessageCommand command)
        {
            // Fail Fast Validations
            command.Validate();
            if (!command.IsValid)
            {
                AddNotifications(command);
                Error error = new(code: ErrorCodes.ERROR_INVALID_SEND_MESSAGE_COMMAND, message: this.GetNotificationsError());
                return new CommandResult(false, error.ToString(), error);
            }

            // Validate author
            User? author = await _userRepository.ReadAsync(command.AuthorId!);
            if (author is null)
            {
                Error error = new(code: ErrorCodes.ERROR_USER_NOT_FOUND, message: string.Format(CommandErrors.ERROR_USER_NOT_FOUND, command.AuthorId));
                return new CommandResult(false, error.ToString(), error);
            }

            // Validate chatroom
            ChatRoom? chatroom = await _chatRoomRepository.ReadAsync(command.ChatRoomId!.Value);
            if (chatroom is null)
            {
                Error error = new(code: ErrorCodes.ERROR_CHATROOM_NOT_FOUND, message: string.Format(CommandErrors.ERROR_CHATROOM_NOT_FOUND, command.ChatRoomId));
                return new CommandResult(false, error.ToString(), error);
            }

            // Check if message is a command
            ChatCommand chatCommand = new();
            chatCommand.Command = command.Message;
            chatCommand.ChatId = chatroom.Id;
            if (chatCommand.IsChatCommand(chatCommand.Command))
                return SendCommandToBot(chatCommand);

            // If the message is not a command proceed with insert
            string messageHash = _cryptographyHelper.EncryptMessage(command.Message!);

            // Create Entity
            Message message = new(
                author: author,
                messageHash: messageHash,
                chatRoom: chatroom);

            // Validate Entity
            AddNotifications(message);
            if (!IsValid)
            {
                Error error = new(code: ErrorCodes.ERROR_INVALID_SEND_MESSAGE_COMMAND, message: this.GetNotificationsError());
                return new CommandResult(false, error.ToString(), error);
            }

            // Register message
            await _messageRepository.CreateAsync(message);

            // Commit changes
            await _unitOfWork.CommitAsync();

            // Send message to MQ
            _mqProducer.PublishMessage(new MessageMQ (messageHash: message.MessageHash, authorName: author.UserName, chatId: chatroom.Id, isErrorMessage: false));

            return new CommandResult(true, CommandValidations.SUCCESS_ON_SEND_MESSAGE_COMMAND);
        }

        private ICommandResult SendCommandToBot(IChatCommand chatCommand)
        {
            Task.Run(() => _bot.ProcessCommand(chatCommand));
            return new CommandResult(true, CommandValidations.SUCCESS_ON_SEND_CHAT_COMMAND_TO_BOT);
        }
    }
}
