using Application.Infrastructure;
using Microsoft.AspNetCore.SignalR;

namespace Application.Hubs
{
    public class MessageHub : Hub
    {
        public async Task SendMessagee(string message)
        {
            if(Clients is not null)
                await Clients.All.SendAsync(HubManager.MESSAGE_HUB_RECEIVE_MESSAGE_METHOD, message);
        }
    }
}
