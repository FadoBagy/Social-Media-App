namespace Social_Media_App.Services.Post
{
    using Social_Media_App.Data.Models;
    using Social_Media_App.Models.Post;

    public interface IPostService
    {
        public void AddPost(Post post);
        public void RemovePost(Post post);
        public void UpdatePost(Post post, FormPostModel model);
        public Task LikePost(int postId, string userId);
        public Task UnlikePost(int postId, string userId);
        public Post CreatePost(string imagePath, string? caption, string userId);
        public Post GetPost(int id);
        public Task<PostViewModel> GetPostForSingleView(int postId, string userId);
        public int GetPostLikeCount(int postId);
        public List<GalleryPostViewModel> GetPostsForGalleryByUserId(string userId);
        public Task<List<PostViewModel>> GetAllPostsForUserFeed(string userId);
        public bool PostIsLikedByUser(int postId, string userId);
    }
}
