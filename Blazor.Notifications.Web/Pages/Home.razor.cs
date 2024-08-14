using Microsoft.AspNetCore.SignalR.Client;

namespace Blazor.Notifications.Web.Pages
{
    public partial class Home : IAsyncDisposable
    {
        private HubConnection? _hubConnection;
        private readonly List<string> _messages = new();

        protected override async Task OnInitializedAsync()
        {
            _hubConnection = new HubConnectionBuilder()
                .WithUrl(
                "https://localhost:7067/notifications",
                o => o.AccessTokenProvider = () => Task.FromResult<string?>("eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6Ikxlb25hcmRvIFNhbnRhbmEgSsO6bmlvciIsInN1YiI6Ikxlb25hcmRvIFNhbnRhbmEgSsO6bmlvciIsImp0aSI6IjU2ZDA0N2IyIiwiYXVkIjpbImh0dHA6Ly9sb2NhbGhvc3Q6MzU4ODEiLCJodHRwczovL2xvY2FsaG9zdDo0NDMwNSIsImh0dHA6Ly9sb2NhbGhvc3Q6NTIyOSIsImh0dHBzOi8vbG9jYWxob3N0OjcwNjciXSwibmJmIjoxNzIzNjMyOTAwLCJleHAiOjE3MzE1ODE3MDAsImlhdCI6MTcyMzYzMjkwMSwiaXNzIjoiZG90bmV0LXVzZXItand0cyJ9.KT_R94Y6V8rIOBF8Z-qk7VGQgZTU7RCvnmhGU2hNAd0"))
                .Build();

            _hubConnection.On<string>("ReceiveNotificationAsync", message =>
            {
                _messages.Add(message);

                InvokeAsync(StateHasChanged);
            });

            await _hubConnection.StartAsync();
        }

        public async ValueTask DisposeAsync()
        {
            if (_hubConnection != null)
            {
                await _hubConnection.DisposeAsync();
            }
        }
    }
}
