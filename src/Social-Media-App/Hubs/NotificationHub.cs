namespace Social_Media_App.Hubs
{
    using Microsoft.AspNetCore.SignalR;
    using Social_Media_App.Hubs.Enums;
    using Social_Media_App.Services.User;
    using static HubsConstants.Chat;

    public class NotificationHub : Hub
    {
        private readonly IUserService users;

        public NotificationHub(IUserService users)
        {
            this.users = users;
        }
        public override async Task OnConnectedAsync()
        {
            var userId = users.GetCurrentUserId();
            await this.users.SaveConnectionIdAsync(userId, Context.ConnectionId, HubType.Notification);

            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            var userId = users.GetCurrentUserId();
            await this.users.RemoveConnectionIdAsync(userId, HubType.Notification);

            await base.OnDisconnectedAsync(exception);
        }

        public async Task SendNotification(string userId)
        {
            var user = await users.GetUserAsync(userId);

            await Clients.User(user.Id).SendAsync(ReceiveNotification);
        }
    }
}
