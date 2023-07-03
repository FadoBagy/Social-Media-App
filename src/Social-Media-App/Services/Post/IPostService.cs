namespace Social_Media_App.Services.Post
{
    using Social_Media_App.Data.Models;
    using Social_Media_App.Models.Post;

    public interface IPostService
    {
        public void AddPost(Post post);
        public Post CreatePost(string imagePath, string? caption, string userId);
        public Post GetPost(int id);
        public List<GalleryPostViewModel> GetPostsForGalleryByUserId(string userId);
        public List<PostViewModel> GetPostsByUserId(string userId);
    }
}
