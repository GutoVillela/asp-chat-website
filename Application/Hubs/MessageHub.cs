using Application.Infrastructure;
using Microsoft.AspNetCore.SignalR;

namespace Application.Hubs
{
    public class MessageHub : Hub
    {
        public async Task SendMessage(string message, string authorName, int chatRoomId)
        {
            if(Clients is not null)
                await Clients.All.SendAsync(HubManager.MESSAGE_HUB_RECEIVE_MESSAGE_METHOD, new { message, authorName, chatRoomId });
        }

        public async Task NotifyError(string message)
        {
            if (Clients is not null)
                await Clients.Caller.SendAsync(HubManager.MESSAGE_HUB_NOTIFY_ERROR_METHOD, new { message });
        }
    }
}
