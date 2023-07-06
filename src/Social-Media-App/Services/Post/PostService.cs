namespace Social_Media_App.Services.Post
{
    using Microsoft.EntityFrameworkCore;
    using Social_Media_App.Data;
    using Social_Media_App.Data.Models;
    using Social_Media_App.Models.Post;
    using System.Collections.Generic;

    public class PostService : IPostService
    {
        private readonly ApplicationDbContext data;
        public PostService(ApplicationDbContext data)
        {
            this.data = data;
        }

        public void AddPost(Post post)
        {
            data.Posts.Add(post);
            data.SaveChanges();
        }

        public Post CreatePost(string imagePath, string? caption, string userId)
        {
            return new Post
            {
                ImagePath = imagePath,
                Caption = caption?.TrimEnd(),
                UserId = userId
            };
        }

        public Post GetPost(int id)
        {
            return data.Posts
                .Include(p => p.User)
                .FirstOrDefault(p => p.Id == id);
        }

        public List<GalleryPostViewModel> GetPostsForGalleryByUserId(string userId)
        {
            return data.Posts
                .Where(p => p.UserId == userId)
                .OrderByDescending(p => p.CreationDate)
                .Select(p => new GalleryPostViewModel
                {
                    Id = p.Id,
                    ImagePath = p.ImagePath,
                    Likes = p.Likes,
                    Comments = p.Comments
                })
                .ToList();
        }

        public List<PostViewModel> GetPostsByUserId(string userId)
        {
            return data.Posts
                .Where(p => p.UserId == userId)
                .OrderByDescending(p => p.CreationDate)
                .Select(p => new PostViewModel
                {
                    ImagePath = p.ImagePath,
                    Caption = p.Caption,
                    CreationDate = p.CreationDate,
                    Likes = p.Likes,
                    Comments = p.Comments
                })
                .ToList();
        }

        public List<PostViewModel> GetAllPosts()
        {
            return data.Posts
                .Include(p => p.User)
                .OrderByDescending(p => p.CreationDate)
                .Select(p => new PostViewModel
                {
                    ImagePath = p.ImagePath,
                    Caption = p.Caption,
                    CreationDate = p.CreationDate,
                    Likes = p.Likes,
                    Comments = p.Comments,
                    User = p.User
                })
                .ToList();
        }
    }
}
