namespace Social_Media_App.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
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
                string currentUserId = User.Id();

                var fileName = file.SaveImage(model.Image, currentUserId).Result;

                var newPost = post.CreatePost(Path.Combine(currentUserId, fileName), model.Caption, currentUserId);
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
                Id = currentPost.Id,
                Caption = currentPost.Caption,
                User = currentPost.User,
                IsSinglePost = true
            });
        }

        public IActionResult Edit(int id)
        {
            var postToEdit = post.GetPost(id);
            var currentUserId = User.Id();

            if (postToEdit?.UserId != currentUserId)
            {
                //TempData["error"] = "You cannot edit this movie!";
                return RedirectToAction("Index", "Home");
            }

            if (postToEdit != null)
            {
                return View(new FormPostModel()
                {
                    Caption = postToEdit.Caption
                });
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        public IActionResult Edit(int id, FormPostModel model)
        {
            var postToEdit = post.GetPost(id);
            var currentUserId = User.Id();
            if (postToEdit?.UserId != currentUserId)
            {
                TempData["error"] = "You cannot edit this movie!";
                return RedirectToAction("Index", "Home");
            }

            if (postToEdit != null)
            {
                post.UpdatePost(postToEdit, model);
            }

            //TempData["edit"] = "Movie information updated successfully, awaiting approval!";
            return RedirectToAction("Profile", "User", new { id = currentUserId });
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            var postToDelete = post.GetPost(id);
            var currentUserId = User.Id();

            if (postToDelete?.UserId != currentUserId)
            {
                //TempData["error"] = "You cannot edit this movie!";
                return RedirectToAction("Index", "Home");
            }

            if (postToDelete != null)
            {
                post.RemovePost(postToDelete);
            }

            //TempData["delete"] = "Post removed successfully!";
            return RedirectToAction("Profile", "User", new { id = currentUserId });
        }
    }
}
