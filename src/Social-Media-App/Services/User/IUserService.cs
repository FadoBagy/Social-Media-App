namespace Social_Media_App.Services.User
{
    using Social_Media_App.Data.Models;
    using Social_Media_App.Hubs.Enums;

    public interface IUserService
    {
        public Task SaveConnectionIdAsync(string userId, string connectionId, HubType hubType);

        public Task RemoveConnectionIdAsync(string userId, HubType hubType);

        public Task<User> GetUserAsync(string userId);

        public Task<User> GetCurrentUserAsync();

        public string GetCurrentUserId();
      
        public User GetUserById(string id);
    }
}
