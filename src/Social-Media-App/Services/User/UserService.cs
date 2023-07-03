namespace Social_Media_App.Services.User
{
    using Social_Media_App.Data;
    using Social_Media_App.Data.Models;

    public class UserService : IUserService
    {
        private readonly ApplicationDbContext data;
        public UserService(ApplicationDbContext data)
        {
            this.data = data;
        }

        public User GetUserById(string id)
        {
            return data.Users.FirstOrDefault(u => u.Id == id);
        }
    }
}
