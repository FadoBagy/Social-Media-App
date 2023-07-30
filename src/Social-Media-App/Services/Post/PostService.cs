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

        public void RemovePost(Post post)
        {
            data.Posts.Remove(post);
            data.SaveChanges();
        }

        public void UpdatePost(Post post, FormPostModel model)
        {
            post.Caption = model?.Caption?.Trim();
            data.SaveChanges();
        }

        public async Task LikePost(int postId, string userId)
        {
            var likedPost = data.Posts
                .Include(p => p.Likes)
                .FirstOrDefault(p => p.Id == postId);

            if (likedPost != null && !likedPost.Likes.Any(l => l.UserId == userId))
            {
                likedPost.Likes.Add(new Like
                {
                    PostId = postId,
                    UserId = userId
                });

                await data.SaveChangesAsync();
            }
        }

        public async Task UnlikePost(int postId, string userId)
        {
            var unlikedPost = data.Posts
                .Include(p => p.Likes)
                .FirstOrDefault(p => p.Id == postId);

            if (unlikedPost != null && unlikedPost.Likes.Any(l => l.UserId == userId))
            {
                var likeToBeRemoved = unlikedPost.Likes.FirstOrDefault(l => l.UserId == userId);
                unlikedPost.Likes.Remove(likeToBeRemoved);
                await data.SaveChangesAsync();
            }
        }

        public Post CreatePost(string imagePath, string? caption, string userId)
        {
            return new Post
            {
                ImagePath = imagePath,
                Caption = caption?.Trim(),
                UserId = userId
            };
        }

        public Post GetPost(int id)
        {
            return data.Posts
                .Include(p => p.User)
                .Include(p => p.Likes)
                .Include(p => p.Comments)
                .FirstOrDefault(p => p.Id == id);
        }

        public async Task<PostViewModel> GetPostForSingleView(int postId, string userId)
        {
            return await data.Posts
                .Include(p => p.User)
                .Include(p => p.Likes)
                .Include(p => p.Comments)
                .Where(p => p.Id == postId)
                .Select(p => new PostViewModel
                {
                    Id = p.Id,
                    ImagePath = p.ImagePath,
                    Caption = p.Caption,
                    CreationDate = p.CreationDate,
                    LikeCount = p.Likes.Count,
                    CommentCount = p.Comments.Count,
                    User = p.User,
                    IsLikedByCurrentUser = p.Likes.Any(l => l.UserId == userId),
                    IsSinglePost = true
                })
                .FirstOrDefaultAsync();
        }

        public int GetPostLikeCount(int postId)
        {
            var currentPost = data.Posts
                .Include(p => p.Likes)
                .FirstOrDefault(p => p.Id == postId);

            if (currentPost != null)
            {
                return currentPost.Likes.Count;
            }

            return 0;
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
                    LikeCount = p.Likes.Count,
                    CommentCount = p.Comments.Count
                })
                .ToList();
        }

        public async Task<List<PostViewModel>> GetAllPostsForUserFeed(string userId)
        {
            var posts = await data.Posts
                .Include(p => p.Likes)
                .OrderByDescending(p => p.CreationDate)
                .Select(p => new PostViewModel
                {
                    Id = p.Id,
                    ImagePath = p.ImagePath,
                    Caption = p.Caption,
                    CreationDate = p.CreationDate,
                    LikeCount = p.Likes.Count,
                    CommentCount = p.Comments.Count,
                    User = p.User,
                    IsLikedByCurrentUser = p.Likes.Any(l => l.UserId == userId)
                })
                .ToListAsync();

            return posts;
        }

        public bool PostIsLikedByUser(int postId, string userId)
        {
            var currentPost = data.Posts
                .Include(p => p.Likes)
                .FirstOrDefault(p => p.Id == postId);

            if (currentPost != null && currentPost.Likes.Any(l => l.UserId == userId))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
