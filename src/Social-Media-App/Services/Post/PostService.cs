namespace Social_Media_App.Services.Post
{
    using Social_Media_App.Data;
    using Social_Media_App.Data.Models;

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
                Caption = caption,
                UserId = userId
            };
        }
    }
}
