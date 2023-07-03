namespace Social_Media_App.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Social_Media_App.Data.Models;
    using Social_Media_App.Infrastructure.Extensions;
    using Social_Media_App.Models.Post;
    using Social_Media_App.Services.File;
    using Social_Media_App.Services.Post;

    [Authorize]
    public class PostController : Controller
    {
        private readonly IPostService post;
        private readonly IFileService file;
        public PostController(
            IPostService post, 
            IFileService file)
        {
            this.post = post;
            this.file = file;
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(FormPostModel model)
        {
            if (ModelState.IsValid)
            {
                string userId = User.Id();

                var fileName = file.SaveImage(model.Image, userId).Result;

                var newPost = post.CreatePost(Path.Combine(userId, fileName), model.Caption, userId);
                post.AddPost(newPost);
            }

            return RedirectToAction("Index", "Home");
        }

        public IActionResult View(int id)
        {
            var currentPost = post.GetPost(id);

            if (currentPost == null)
            {
                return NotFound();
            }
            return View(new PostViewModel
            {
                ImagePath = currentPost.ImagePath,
                Caption = currentPost.Caption,
                CreationDate = currentPost.CreationDate,
                Likes = currentPost.Likes,
                Comments = currentPost.Comments,
                User = currentPost.User
            });
        }
    }
}
