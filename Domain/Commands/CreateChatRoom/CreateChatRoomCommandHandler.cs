using Domain.Commands.Base;
using Domain.Entities;
using Domain.Repositories;
using Flunt.Notifications;
using Shared.Commands;
using Shared.Constants;
using Shared.Extensions;
using Shared.Handlers;
using Shared.Repositories;
using Shared.Validations;
using Shared.ValueObjects;

namespace Domain.Commands.CreateChatRoom
{
    public class CreateChatRoomCommandHandler : Notifiable<Notification>, IValidatable<Notification>, ICommandHandler<CreateChatRoomCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IChatRoomRepository _chatRoomRepository;
        private readonly IUserRepository _userRepository;

        public CreateChatRoomCommandHandler(IUnitOfWork unitOfWork, IChatRoomRepository chatRoomRepository, IUserRepository userRepository)
        {
            _unitOfWork = unitOfWork;
            _chatRoomRepository = chatRoomRepository;
            _userRepository = userRepository;
        }

        public async Task<ICommandResult> HandleAsync(CreateChatRoomCommand command)
        {
            // Fail Fast Validations
            command.Validate();
            if (!command.IsValid)
            {
                AddNotifications(command);
                Error error = new(code: ErrorCodes.ERROR_INVALID_CREATE_CHATROOM_COMMAND, message: this.GetNotificationsError());
                return new CommandResult(false, error.ToString(), error);
            }

            // Validate user
            User? user = await _userRepository.ReadAsync(command.UserId!);
            if(user is null)
            {
                Error error = new(code: ErrorCodes.ERROR_USER_NOT_FOUND, message: string.Format(CommandErrors.ERROR_USER_NOT_FOUND, command.UserId));
                return new CommandResult(false, error.ToString(), error);
            }

            // Create Entity
            ChatRoom chatRoom = new(
                name: command.Name!,
                users: new HashSet<User> { user }
                );

            // Validate Entity
            AddNotifications(chatRoom);
            if (!IsValid)
            {
                Error error = new(code: ErrorCodes.ERROR_INVALID_CREATE_CHATROOM_COMMAND, message: this.GetNotificationsError());
                return new CommandResult(false, error.ToString(), error);
            }

            // Register chatroom
            await _chatRoomRepository.CreateAsync(chatRoom);

            // Commit changes
            await _unitOfWork.CommitAsync();

            return new CommandResult(true, CommandValidations.SUCCESS_ON_CREATE_CHATROOM_COMMAND);
        }
    }
}
