namespace Blazor.Notifications.API.Hubs.Interfaces
{
    public interface INotificationClient
    {
        Task ReceiveNotificationAsync(string message);
    }
}
