namespace Social_Media_App.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Social_Media_App.Models.User;
    using Social_Media_App.Services.Post;
    using Social_Media_App.Services.User;

    public class UserController : Controller
    {
        private readonly IUserService user;
        private readonly IPostService post;
        public UserController(
            IUserService user,
            IPostService post)
        {
            this.user = user;
            this.post = post;
        }

        public IActionResult Profile(string id)
        {
            var currentUser = user.GetUserById(id);
            var userPosts = post.GetPostsForGalleryByUserId(id);

            return View(new UserProfileViewModel
            {
                User = new UserViewModel
                {
                    Id = currentUser.Id,
                    Username = currentUser.UserName
                },
                Posts = userPosts
            });
        }
    }
}
