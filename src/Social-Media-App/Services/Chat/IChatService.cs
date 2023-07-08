namespace Social_Media_App.Services.Chat
{
    public interface IChatService
    {
        public Task<bool> ChatExistsAsync(string chatTitle);

        public Task CreateChatAsync(string title);

        public Task AddUserToChatAsync(string userId, string chatTitle);
    }
}
