namespace Social_Media_App.Hubs
{
    using Microsoft.AspNetCore.SignalR;
    using Social_Media_App.Services.User;
    using Social_Media_App.Hubs.Enums;
    using static HubsConstants.Chat;

    public class ChatHub : Hub
    {
        private readonly IUserService users;
        private readonly IHubContext<NotificationHub> notificationHub;

        public ChatHub(IUserService users,
            IHubContext<NotificationHub> notificationHub)
        {
            this.users = users;
            this.notificationHub = notificationHub;
        }

        public override async Task OnConnectedAsync()
        {
            var userId = users.GetCurrentUserId();
            await this.users.SaveConnectionIdAsync(userId, Context.ConnectionId, HubType.Chat);

            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            var userId = users.GetCurrentUserId();
            await this.users.RemoveConnectionIdAsync(userId, HubType.Chat);

            await base.OnDisconnectedAsync(exception);
        }

        public async Task SendMessage()
        {
            var userId = "76841979-fa41-4651-ac38-40d676727742";
            var user = await users.GetUserAsync(userId);

            if (user.ChatHubConnectionId != null)
            {
                await Clients.User(user.Id).SendAsync("ReceiveMessage");
            }
            else
            {
                await notificationHub.Clients.User(user.Id).SendAsync(ReceiveNotification);
            }
        }
    }
}
