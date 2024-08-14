using Blazor.Notifications.API.Hubs.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace Blazor.Notifications.API.Hubs
{
    [Authorize]
    public class NotificationsHub : Hub<INotificationClient>
    {
        public override async Task OnConnectedAsync()
        {
            await Clients.Client(Context.ConnectionId).ReceiveNotificationAsync(
                $"{Context.User?.Identity?.Name} connected to Notification Hub.");

            await base.OnConnectedAsync();
        }
    }
}
