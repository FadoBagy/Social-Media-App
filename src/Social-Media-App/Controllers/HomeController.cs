namespace Social_Media_App.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Social_Media_App.Models.Home;
    using Social_Media_App.Services.Post;

    [Authorize]
    public class HomeController : Controller
    {
        private readonly IPostService post;
        public HomeController(IPostService post)
        {
            this.post = post;
        }

        public IActionResult Index()
        {
            var allPosts = post.GetAllPosts();

            return View(new HomeViewModel
            {
                Posts = allPosts
            });
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [AllowAnonymous]
        public IActionResult Error()
        {
            return View();
        }
    }
}