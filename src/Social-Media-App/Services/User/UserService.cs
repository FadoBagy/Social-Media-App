namespace Social_Media_App.Services.User
{
    using Microsoft.EntityFrameworkCore;
    using Social_Media_App.Data;
    using Social_Media_App.Data.Models;
    using Social_Media_App.Hubs.Enums;
    using System.Security.Claims;

    public class UserService : IUserService
    {
        private readonly ApplicationDbContext data;
        private readonly IHttpContextAccessor httpContextAccessor;
        public UserService(ApplicationDbContext data,
            IHttpContextAccessor httpContextAccessor)
        {
            this.data = data;
            this.httpContextAccessor = httpContextAccessor;
        }
        public async Task SaveConnectionIdAsync(string userId, string connectionId, HubType hubType)
        {
            var user = await this.data
                .Users
                .Where(u => u.Id == userId)
                .FirstOrDefaultAsync();

            if (user != null)
            {
                if (hubType == HubType.Chat)
                {
                    user.ChatHubConnectionId = connectionId;
                }
                else if (hubType == HubType.Notification)
                {
                    user.NotificationHubConnectionId = connectionId;
                }
            }

            await this.data.SaveChangesAsync();
        }

        public async Task RemoveConnectionIdAsync(string userId, HubType hubType)
        {
            var user = await this.data
                .Users
                .Where(u => u.Id == userId)
                .FirstOrDefaultAsync();

            if (user != null)
            {
                if (hubType == HubType.Chat)
                {
                    user.ChatHubConnectionId = null;
                }
                else if (hubType == HubType.Notification)
                {
                    user.NotificationHubConnectionId = null;
                }
            }

            await this.data.SaveChangesAsync();
        }

        public async Task<User> GetUserAsync(string userId)
        {
            var user = await this.data
                .Users
                .Where(u => u.Id == userId)
                .FirstOrDefaultAsync();

            return user;
        }

        public async Task<User> GetCurrentUserAsync()
        {
            var userId = GetCurrentUserId();

            return await this.data
                .Users
                .Where(u => u.Id == userId)
                .FirstOrDefaultAsync();
        }

        public string GetCurrentUserId()
        {
            var user = httpContextAccessor.HttpContext.User;
            var userId = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            return userId;
        }
    }
}
