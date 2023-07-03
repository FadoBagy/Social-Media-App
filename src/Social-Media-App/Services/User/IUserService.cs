namespace Social_Media_App.Services.User
{
    using Social_Media_App.Data.Models;

    public interface IUserService
    {
        public User GetUserById(string id);
    }
}
