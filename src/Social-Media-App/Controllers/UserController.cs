namespace Social_Media_App.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Social_Media_App.Models.User;
    using Social_Media_App.Services.User;

    public class UserController : Controller
    {
        private readonly IUserService user;
        public UserController(IUserService user)
        {
            this.user = user;
        }

        public IActionResult Profile(string id)
        {
            var currentUser = user.GetUserById(id);

            return View(new UserProfileViewModel
            {
                User = new UserViewModel
                {
                    Id = currentUser.Id,
                    Username = currentUser.UserName
                }
            });
        }
    }
}
