
using Blazor.Notifications.API.Hubs;
using Blazor.Notifications.API.Hubs.Interfaces;
using Microsoft.AspNetCore.SignalR;

namespace Blazor.Notifications.API.BackgroundServices
{
    public class ServerTimeNotifier(IHubContext<NotificationsHub, INotificationClient> context, ILogger<ServerTimeNotifier> logger) : BackgroundService
    {
        private static readonly TimeSpan Period = TimeSpan.FromSeconds(5);
        private readonly ILogger<ServerTimeNotifier> _logger = logger;
        private readonly IHubContext<NotificationsHub, INotificationClient> _context = context;

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            using var timer = new PeriodicTimer(Period);

            while (!stoppingToken.IsCancellationRequested &&
                await timer.WaitForNextTickAsync(stoppingToken)) 
            {
                var dateTime = DateTime.Now;

                _logger.LogInformation("Executing {Service} {Time}", nameof(ServerTimeNotifier), dateTime);

                await _context.Clients.User("Leonardo Santana Júnior").ReceiveNotificationAsync($"Server time = {dateTime}");
            }
        }
    }
}
