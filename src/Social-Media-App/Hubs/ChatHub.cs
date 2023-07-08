namespace Social_Media_App.Hubs
{
    using Microsoft.AspNetCore.SignalR;
    using Social_Media_App.Services.User;
    using Social_Media_App.Hubs.Enums;
    using Social_Media_App.Services.Chat;
    using static HubsConstants.Chat;

    public class ChatHub : Hub
    {
        private readonly IUserService users;
        private readonly IChatService chats;
        private readonly IHubContext<NotificationHub> notificationHub;

        public ChatHub(IUserService users,
            IChatService chats,
            IHubContext<NotificationHub> notificationHub)
        {
            this.users = users;
            this.chats = chats;
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

        public async Task SendMessage(string message, string senderId)
        {
            var userId = "76841979-fa41-4651-ac38-40d676727742";
            var user = await users.GetUserAsync(userId);

            if (user.ChatHubConnectionId != null)
            {
                var chatTitle = CreateChatTitle(userId);
                var chatExists = await chats.ChatExistsAsync(chatTitle);

                if (!chatExists)
                {
                    await CreateChat(user.ChatHubConnectionId, user.Id, chatTitle);
                }
                await Groups.AddToGroupAsync(Context.ConnectionId, chatTitle);
                await Groups.AddToGroupAsync(user.ChatHubConnectionId, chatTitle);

                await Clients.Group(chatTitle).SendAsync("ReceiveMessage", senderId, message);
            }
            else
            {
                await notificationHub.Clients.User(user.Id).SendAsync(ReceiveNotification);
            }
        }

        public async Task CreateChat(string receiverConnectionId,
            string receiverUserId,
            string chatTitle)
        {
            var currentUserConnectionId = Context.ConnectionId;
            if (receiverConnectionId != null && currentUserConnectionId != null)
            {
                var currentUserId = users.GetCurrentUserId();

                await chats.CreateChatAsync(chatTitle);
                await chats.AddUserToChatAsync(currentUserId, chatTitle);
                await chats.AddUserToChatAsync(receiverUserId, chatTitle);
            }
        }

        private string CreateChatTitle(string receiverUserId)
        {
            var currentUserId = users.GetCurrentUserId();

            var idOfChatMembers = new List<string> { currentUserId, receiverUserId };
            idOfChatMembers.Sort();

            var chatName = idOfChatMembers[0] + "-" + idOfChatMembers[1];

            return chatName;
        }
    }
}
