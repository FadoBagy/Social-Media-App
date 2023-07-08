namespace Social_Media_App.Services.Chat
{
    using Microsoft.EntityFrameworkCore;
    using Social_Media_App.Data;
    using System.Threading.Tasks;
    using Social_Media_App.Data.Models;

    public class ChatService : IChatService
    {
        private readonly ApplicationDbContext data;

        public ChatService(ApplicationDbContext data)
        {
            this.data = data;
        }

        public async Task<bool> ChatExistsAsync(string chatTitle)
        {
            var chatExists = await this.data
                .Chats
                .Where(c => c.Title == chatTitle)
                .AnyAsync();

            return chatExists;
        }

        public async Task CreateChatAsync(string title)
        {
            var chat = new Chat { Title = title };
            await this.data
                .Chats
                .AddAsync(chat);

            await this.data.SaveChangesAsync();
        }

        public async Task AddUserToChatAsync(string userId, string chatTitle)
        {
            var chat = await this.data
                .Chats
                .Where(c => c.Title == chatTitle)
                .FirstOrDefaultAsync();

            var userIsInChat = chat
                .Users
                .Any(u => u.Id == userId);

            if (!userIsInChat)
            {
                var currentUser = await this.data
                    .Users
                    .Where(u => u.Id == userId)
                    .FirstOrDefaultAsync();

                chat
                    .Users
                    .Add(currentUser);

                //var userchat = new UserChats
                //{
                //    UserId = userId,
                //    ChatId = chat.Id
                //};

                //this.data
                //    .UserChats
                //    .Add(userchat);

                await this.data.SaveChangesAsync();
            }
        }
    }
}
