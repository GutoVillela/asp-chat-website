﻿using Application.Models;
using Shared.Commands;

namespace Application.Services.Interfaces
{
    public interface IChatRoomApplicationService
    {
        Task<ICommandResult> CreateChatRoomAsync(ChatRoomViewModel chatRoom);
    }
}
