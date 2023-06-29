namespace Social_Media_App.Services.Post
{
    using Social_Media_App.Data.Models;

    public interface IPostService
    {
        public void AddPost(Post post);
        public Post CreatePost(string imagePath, string? caption, string userId);
    }
}
